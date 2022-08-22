import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

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

  eventFilter(filterFor: string): any {
    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local : string}) => evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  hideImg() {
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.http.get('https://localhost:44365/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = response;
      },
      error => console.log(error)
    );
  }
}
