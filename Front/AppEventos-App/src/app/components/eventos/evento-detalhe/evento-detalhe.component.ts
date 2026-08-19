import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { EventoService } from './../../../services/evento.service';
import { Evento } from '@app/models/evento';
import { Lote } from '@app/models/lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { LoteDetalheComponent } from '../lote-detalhe/lote-detalhe.component';
import { take } from 'rxjs';

@Component({
    selector: 'app-evento-detalhe',
    templateUrl: './evento-detalhe.component.html',
    styleUrls: ['./evento-detalhe.component.scss'],
    standalone: false
})
export class EventoDetalheComponent implements OnInit {
  modalRef?: BsModalRef;
  eventoId: number = 0;
  locale = 'pt-br';
  form: FormGroup = new FormGroup({});
  evento = {} as Evento;
  lotesList: Lote[] = [];
  loteAtual = { id: 0, nome: '' };
  entityState = 'post';
  datePickerConfig: Partial<BsDatepickerConfig> = {
    adaptivePosition: true,
    dateInputFormat: 'DD/MM/YYYY',
    containerClass: 'theme-default',
    showWeekNumbers: false
  };

  get editMode(): boolean {
    return this.eventoId > 0;
  }

  get fc(): any {
    return this.form.controls;
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

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private actRoute: ActivatedRoute,
              private eventoService: EventoService,
              private loteService: LoteService,
              private spinner: NgxSpinnerService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private router: Router)
   {
    this.localeService.use(this.locale);
   }

   public loadEvent(): void {
    const eventId = this.actRoute.snapshot.paramMap.get('id');
    this.eventoId = eventId ? +eventId : 0;

    if((this.eventoId !== null) || (this.eventoId == 0)){
      this.entityState = 'put';
      this.spinner.show();

      this.eventoService.getEventoById(this.eventoId).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
          this.loadLotes();
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());
    }
   }

  ngOnInit(): void {
    this.validation();
    this.loadEvent();
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required]
    });
  }

  public loadLotes(): void {
    if (this.eventoId <= 0)
      return;

    this.loteService.getLotesByEventoId(this.eventoId).subscribe({
      next: (lotes: Lote[]) => {
        this.lotesList = lotes;
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar carregar lotes.', 'Erro!');
        console.error(error);
      }
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(fieldForm: FormControl | AbstractControl | null): any {
    if (!fieldForm)
      return '';

    return { 'is-invalid': fieldForm.errors && fieldForm.touched}
  }

  public saveChanges(): void {
    this.spinner.show();
    if (this.form.valid) {

      this.evento = (this.entityState === 'post')
          ? { ...this.form.value }
          : {id: this.evento.id, ...this.form.value };

      const service = (this.entityState === 'post')
        ? this.eventoService.post(this.evento)
        : this.eventoService.put(this.evento);

      service.subscribe({
        next: (event: Evento) =>{
          this.toastr.success('Evento salvo com sucesso!', 'Sucesso!');
          this.router.navigate([`eventos/detalhe/${event.id}`]);
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());
    }
  }

  public newLote(): void {
    this.openLoteModal(0);
  }

  public detalheLote(loteId: number): void {
    this.openLoteModal(loteId);
  }

  private openLoteModal(loteId: number): void {
    this.modalRef = this.modalService.show(LoteDetalheComponent, {
      class: 'modal-lg',
      initialState: {
        eventoId: this.eventoId,
        loteId: loteId
      }
    });

    this.modalRef.onHidden?.pipe(take(1)).subscribe(() => {
      this.loadLotes();
    });
  }

  public openDeleteModal(event: Event, template: TemplateRef<any>, lote: Lote): void {
    event.stopPropagation();
    this.loteAtual = { id: lote.id, nome: lote.nome };
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDelete(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.loteService.delete(this.eventoId, this.loteAtual.id).subscribe({
      next: () => {
        this.toastr.success('Lote excluído com sucesso!', 'Sucesso!');
        this.loadLotes();
      },
      error: (error: any) => {
        this.toastr.error('Erro ao tentar excluir lote.', 'Erro!');
        console.error(error);
      }
    }).add(() => this.spinner.hide());
  }

  public declineDelete(): void {
    this.modalRef?.hide();
  }
}
