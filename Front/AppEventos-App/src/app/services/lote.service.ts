import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/lote';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';

@Injectable()
export class LoteService {
  baseURL = environment.apiUrl + '/lotes';

  constructor(private http: HttpClient) { }

  public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
    return this.http
      .get<Lote[]>(`${this.baseURL}/${eventoId}`)
      .pipe(take(1));
  }

  public getLoteByEventoIdAndLoteId(eventoId: number, loteId: number): Observable<Lote> {
    return this.http
      .get<Lote>(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }

  public saveLotes(eventoId: number, Lotes: Lote[]): Observable<Lote[]> {
    return this.http
      .put<Lote[]>(`${this.baseURL}/${eventoId}`, Lotes)
      .pipe(take(1));
  }

  public delete(eventoId: number, loteId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }

  public saveLote(eventoId: number, lote: Lote): Observable<Lote> {
    return this.http
      .put<Lote>(`${this.baseURL}/${eventoId}`, lote)
      .pipe(take(1));
  }
}
