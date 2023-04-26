import { Evento } from "./evento";

export interface Lote {
   Id: number;
   Nome: string;
   Preco: number;
   DataInicio?: Date;
   DataFim?: Date;
   Quantidade: number;
   EventoId: number;
   Evento: Evento;
}
