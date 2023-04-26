import { Lote } from "./lote";
import { Palestrante } from "./palestrante";
import { RedeSocial } from "./redesocial";

export interface Evento {
 Id: number;
 Local: string;
 DataEvento?: Date;
 Tema: string;
 QtdPessoas: number;
 ImagemURL: string;
 Telefone: string;
 Email: string;
 Lotes: Lote[];
 RedesSociais: RedeSocial[];
 PalestrantesEventos: Palestrante;
}
