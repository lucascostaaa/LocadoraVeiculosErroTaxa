import { Observable } from "rxjs";
import { TaxaCreateViewModel } from "../viewModels/taxa/TaxaCreateViewModel";
import { TaxaDetailsViewModel } from "../viewModels/taxa/TaxaDetailsViewModel";
import { TaxaEditViewModel } from "../viewModels/taxa/TaxaEditViewModel";
import { TaxaListViewModel } from "../viewModels/taxa/TaxaListViewModel";

export interface IHttpTaxaService {

    obterTaxa(): Observable<TaxaListViewModel[]>

    adicionarTaxa(taxa: TaxaCreateViewModel): Observable<TaxaCreateViewModel>

    obterTaxaPorId(taxaId: number): Observable<TaxaDetailsViewModel>

    editarTaxa(taxa: TaxaEditViewModel): Observable<TaxaEditViewModel>

    excluirTaxa(taxaId: number): Observable<number>
}