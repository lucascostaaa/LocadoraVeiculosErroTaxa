import { ClienteType } from "./ClienteEnum";

export class Cliente{

    id: number;
    nome: string;
    endereco : string;
    telefone : string;
    rg :string;
    cpf: string;
    cnpj: string;
    email: string;
    tipoPessoa : ClienteType;
}