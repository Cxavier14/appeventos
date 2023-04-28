import { Component, OnInit } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers: [EventoService] option of dependency injectable
})
export class EventosComponent implements OnInit {

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

  constructor(private eventoService: EventoService) { }

  ngOnInit(): void {
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
      error: (error: any) => console.log(error)
    });
  }
}
