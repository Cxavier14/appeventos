import { RedeSocial } from "./redesocial";

export interface Palestrante {
  Id: number;
  Nome: string;
  MiniCurriculo: string;
  ImagemURL: string;
  Telefone: string;
  Email: string;
  RedesSociais: RedeSocial[];
}
