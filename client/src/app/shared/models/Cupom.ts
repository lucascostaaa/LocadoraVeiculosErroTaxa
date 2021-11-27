import { CupomType } from "./CupomEnum";
import { Parceiro } from "./Parceiro";

export class Cupom {
    id: number;
    nome: string;
    valor: number;
    valorMinimo: number;
    dataValidade: Date;
    parceiro: Parceiro;
    tipo: CupomType;
}