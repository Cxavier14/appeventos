import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/eventos')

    this.eventos = [
      {
        Tema: "Angular",
        Local: "Blumenau"
      },
      {
        Tema: ".NET 5",
        Local: "Brusque"
      },
      {
        Tema: "Angular com .NET 5",
        Local: "Indaial"
      }
    ]
  }
}
