import { Observable } from "rxjs";
import { ParceiroCreateViewModel } from "../viewModels/parceiro/ParceiroCreateViewModel";
import { ParceiroDetailsViewModel } from "../viewModels/parceiro/ParceiroDetailsViewModel";
import { ParceiroEditViewModel } from "../viewModels/parceiro/ParceiroEditViewModel";
import { ParceiroListViewModel } from "../viewModels/parceiro/ParceiroListViewModel";

export interface IHttpParceiroService {

    obterParceiros(): Observable<ParceiroListViewModel[]>

    adicionarParceiro(parceiro: ParceiroCreateViewModel): Observable<ParceiroCreateViewModel>

    obterParceiroPorId(parceiroId: number): Observable<ParceiroDetailsViewModel>

    editarParceiro(parceiro: ParceiroEditViewModel): Observable<ParceiroEditViewModel>

    excluirParceiro(parceiroId: number): Observable<number>
}