import { Observable } from "rxjs";
import { CupomCreateViewModel } from "../viewModels/cupom/CupomCreateViewModel";
import { CupomDetailsViewModel } from "../viewModels/cupom/CupomDetailsViewModel";
import { CupomEditViewModel } from "../viewModels/cupom/CupomEditViewModel";
import { CupomListViewModel } from "../viewModels/cupom/CupomListViewModel";

export interface IHttpCupomService {

    obterCupons(): Observable<CupomListViewModel[]>

    adicionarCupom(cupom: CupomCreateViewModel): Observable<CupomCreateViewModel>

    obterCupomPorId(cupomId: number): Observable<CupomDetailsViewModel>

    editarCupom(cupom: CupomEditViewModel): Observable<CupomEditViewModel>

    excluirCupom(cupomId: number): Observable<number>
}