import { LoteService } from './../../../services/lote.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Lote } from '@app/models/lote';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lote-detalhe',
  templateUrl: './lote-detalhe.component.html',
  styleUrls: ['./lote-detalhe.component.scss'],
  standalone: false
})

export class LoteDetalheComponent implements OnInit {
  modalRef?: BsModalRef;
  loteId: number = 0;
  locale = 'pt-br';
  form: FormGroup = new FormGroup({});
  lote = {} as Lote;
  datePickerConfig: Partial<BsDatepickerConfig> = {
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };

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
    private actRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private loteService: LoteService,
    private toastr: ToastrService,
    private router: Router) {
    this.localeService.use(this.locale);
  }

  ngOnInit(): void {
    this.loadLote();
    this.validation();
  }

  public cssValidator(fieldForm: FormControl | AbstractControl | null): any {
    if (!fieldForm)
      return '';

    return { 'is-invalid': fieldForm.errors && fieldForm.touched }
  }

  public loadLote(): void {
    const loteId = this.actRoute.snapshot.paramMap.get('loteId');
    this.loteId = loteId ? +loteId : 0;

    const eventoId = this.actRoute.snapshot.paramMap.get('eventoId');
    this.eventoId = eventoId ? +eventoId : 0;

    if (((this.loteId !== null) && (this.eventoId !== null))
      || ((this.loteId == 0) && (this.eventoId == 0))) {
      this.spinner.show();

      this.loteService.getLoteByEventoIdAndLoteId(this.eventoId, this.loteId).subscribe({
        next: (lote: Lote) => {
          this.lote = { ...lote };
          this.form.patchValue(this.lote);
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar carregar o lote.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      preco: ['', Validators.required],
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required],
      quantidade: ['', [Validators.required, Validators.max(120000)]]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public saveChanges(): void {
      this.spinner.show();
      if (this.form.valid) {

        this.lote = (this.entityState === 'post')
            ? { ...this.form.value }
            : {id: this.lote.id, ...this.form.value };

        const service = (this.entityState === 'post')
          ? this.loteService.post(this.evento)
          : this.eventoService.put(this.evento);

        this.loteService.saveLote(this.eventoId, this.lote).subscribe({
          next: (lote: Lote) =>{
            this.toastr.success('Lote salvo com sucesso!', 'Sucesso!');
            this.router.navigate([`eventos/detalhe/${lote.eventoId}`]);
          },
          error: (error: any) => {
            this.toastr.error('Erro ao tentar salvar o lote.', 'Erro!');
            console.error(error);
          }
        }).add(() => this.spinner.hide());
      }
    }
};


