
import { EstadoTaxaLocacao } from "./EstadoLocacaoEnum";
import { TaxaType } from "./TaxaEnum";

export class Taxa {
    id: number;
    nome: string;
    valor: number;
    tipoTaxa: TaxaType;
    estadoLocacao: EstadoTaxaLocacao;
}