import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { EventoService } from './../../../services/evento.service';
import { Evento } from '@app/models/evento';

@Component({
    selector: 'app-evento-detalhe',
    templateUrl: './evento-detalhe.component.html',
    styleUrls: ['./evento-detalhe.component.scss'],
    standalone: false
})
export class EventoDetalheComponent implements OnInit {
  locale = 'pt-br'
  form: UntypedFormGroup = new UntypedFormGroup({});
  evento = {} as Evento;
  entityState = 'post';

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

  constructor(private fb: UntypedFormBuilder, private localeService: BsLocaleService,
              private actRoute: ActivatedRoute, private eventoService: EventoService,
              private spinner: NgxSpinnerService, private toastr: ToastrService)
   {
    this.localeService.use(this.locale);
   }

   public loadEvent(): void {
    const eventId = this.actRoute.snapshot.paramMap.get('id');

    if(eventId !== null){
      this.entityState = 'put';
      this.spinner.show();
      this.eventoService.getEventoById(+eventId).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          console.error(error);
        },
        complete: () => this.spinner.hide()
        }        
      );
    }
   }

  ngOnInit(): void {
    this.loadEvent();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(fieldForm: UntypedFormControl): any {
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
        next: () => 
          this.toastr.success('Evento salvo com sucesso!', 'Sucesso!'),
        error: (error: any) => {          
          this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());
    }
  }
}
