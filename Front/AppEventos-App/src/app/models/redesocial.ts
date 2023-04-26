import { Evento } from "./evento";
import { Palestrante } from "./palestrante";

export interface RedeSocial {
  Id: number;
  Nome: string
  URL: string
  EventoId?: number;
  Evento: Evento;
  PalestranteId?: number;
  Palestrante: Palestrante;
}
