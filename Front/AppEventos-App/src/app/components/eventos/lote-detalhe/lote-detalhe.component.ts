import { ChangeDetectorRef, Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Lote } from '@app/models/lote';
import { LoteService } from '@app/services/lote.service';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { merge, Subscription } from 'rxjs';

@Component({
  selector: 'app-lote-detalhe',
  templateUrl: './lote-detalhe.component.html',
  styleUrls: ['./lote-detalhe.component.scss'],
  providers: [CurrencyPipe],
  standalone: false
})
export class LoteDetalheComponent implements OnInit, OnDestroy {
  eventoId = 0;
  loteId = 0;
  locale = 'pt-br';
  form: FormGroup = new FormGroup({});
  lote = {} as Lote;
  deleteModalRef?: BsModalRef;
  precoDisplay = '';
  private formChangesSub?: Subscription;
  datePickerConfig: Partial<BsDatepickerConfig> = {
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };

  get editMode(): boolean {
    return this.loteId > 0;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  get fc(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private spinner: NgxSpinnerService,
    private loteService: LoteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private cdr: ChangeDetectorRef,
    private currencyPipe: CurrencyPipe,
    public bsModalRef: BsModalRef) {
    this.localeService.use(this.locale);
  }

  ngOnInit(): void {
    this.validation();
    this.watchFormChanges();

    if (this.loteId > 0) {
      this.loadLote();
    }
  }

  ngOnDestroy(): void {
    this.formChangesSub?.unsubscribe();
  }

  private watchFormChanges(): void {
    this.formChangesSub = merge(this.form.statusChanges, this.form.valueChanges).subscribe(() => {
      this.cdr.detectChanges();
    });
  }

  public cssValidator(fieldForm: FormControl | AbstractControl | null): any {
    if (!fieldForm)
      return '';

    return { 'is-invalid': fieldForm.errors && fieldForm.touched }
  }

  public loadLote(): void {
    this.spinner.show();

    this.loteService.getLoteByEventoIdAndLoteId(this.eventoId, this.loteId).subscribe({
      next: (lote: Lote) => {
        this.lote = { ...lote };
        this.form.patchValue({
          ...this.lote,
          dataInicio: this.toDate(this.lote.dataInicio),
          dataFim: this.toDate(this.lote.dataFim)
        });
        this.precoDisplay = this.formatPreco(this.fc.preco.value);
        this.cdr.detectChanges();
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar carregar o lote.', 'Erro!');
        console.error(error);
      }
    }).add(() => this.spinner.hide());
  }

  public validation(): void {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      preco: [null, [Validators.required, Validators.min(0.01)]],
      dataInicio: [null, Validators.required],
      dataFim: [null, Validators.required],
      quantidade: [null, [Validators.required, Validators.min(1), Validators.max(120000)]]
    });
  }

  public updatePreco(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.precoDisplay = input.value;
    const value = this.precoDisplay.replace(/[^0-9,.-]/g, '').replace(/\./g, '').replace(',', '.');
    const preco = value ? Number(value) : null;

    this.fc.preco.setValue(Number.isNaN(preco) ? null : preco);
  }

  public focusPreco(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.precoDisplay = this.fc.preco.value ?? '';
    input.value = this.precoDisplay;
  }

  public blurPreco(): void {
    this.precoDisplay = this.formatPreco(this.fc.preco.value);
    this.fc.preco.markAsTouched();
  }

  private formatPreco(value: number | null): string {
    return value === null || value === undefined
      ? ''
      : this.currencyPipe.transform(value, 'BRL', 'symbol', '1.2-2', 'pt-BR') ?? '';
  }

  private toDate(value?: Date): Date | null {
    if (!value) {
      return null;
    }

    const date = value instanceof Date ? value : new Date(value);
    return isNaN(date.getTime()) ? null : date;
  }

  public cancel(): void {
    this.bsModalRef.hide();
  }

  public saveChanges(): void {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      this.cdr.detectChanges();
      return;
    }

    this.spinner.show();

    const lote: Lote = this.editMode
      ? { id: this.lote.id, eventoId: this.eventoId, ...this.form.value }
      : { id: 0, eventoId: this.eventoId, ...this.form.value };

    this.loteService.saveLote(this.eventoId, lote).subscribe({
      next: () => {
        this.toastr.success('Lote salvo com sucesso!', 'Sucesso!');
        this.bsModalRef.hide();
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar salvar o lote.', 'Erro!');
        console.error(error);
      }
    }).add(() => this.spinner.hide());
  }

  public openDeleteModal(template: TemplateRef<any>): void {
    this.deleteModalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDelete(): void {
    this.deleteModalRef?.hide();
    this.spinner.show();

    this.loteService.delete(this.eventoId, this.loteId).subscribe({
      next: () => {
        this.toastr.success('Lote excluído com sucesso!', 'Sucesso!');
        this.bsModalRef.hide();
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar excluir lote.', 'Erro!');
        console.error(error);
      }
    }).add(() => this.spinner.hide());
  }

  public declineDelete(): void {
    this.deleteModalRef?.hide();
  }
}
