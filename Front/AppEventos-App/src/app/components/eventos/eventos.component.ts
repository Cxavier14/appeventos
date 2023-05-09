import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Evento } from '../../models/evento';
import { EventoService } from '../../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers: [EventoService] option of dependency injectable
})
export class EventosComponent implements OnInit {

  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  widthImg: number = 150;
  marginImg: number = 2;
  showImg: boolean = true;
  private _listFilter: string = "";

  public get listFilter(): string {
    return this._listFilter;
  }

  public set listFilter(value: string){
    this._listFilter = value;
    this.eventosFiltrados = this.listFilter ? this.eventFilter(this.listFilter) : this.eventos;
  }

  public eventFilter(filterFor: string): Evento[] {
    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local : string}) => evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    )
  }

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService
              ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public hideImg(): void {
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (_eventos: Evento[] ) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar Eventos', 'Erro');
      },
      complete: () => { this.spinner.hide(); }
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.toastr.success('Evento deletado com sucesso!', 'Deletado!');
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
