import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { EventoService } from './../../../services/evento.service';
import { Evento } from '@app/models/evento';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  locale = 'pt-br'
  form: FormGroup = new FormGroup({});
  evento = {} as Evento;

  get fc(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true, adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a', containerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder, private localeService: BsLocaleService,
              private actRoute: ActivatedRoute, private eventoService: EventoService,
              private spinner: NgxSpinnerService, private toastr: ToastrService)
   {
    this.localeService.use(this.locale);
   }

   public loadEvent(): void {
    const eventId = this.actRoute.snapshot.paramMap.get('id');

    if(eventId !== null){
      this.spinner.show();
      this.eventoService.getEventoById(+eventId).subscribe(
        (evento: Evento) => {
          this.evento = {...evento};
          this.form.patchValue(evento);
        },
        (error) => {
          this.spinner.hide();
          this.toastr.error('Erro ao carregar Evento', 'Erro!');
          console.error(error);
        },
        () => this.spinner.hide()
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

  public cssValidator(fieldForm: FormControl): any {
    return { 'is-invalid': fieldForm.errors && fieldForm.touched}
  }
}
