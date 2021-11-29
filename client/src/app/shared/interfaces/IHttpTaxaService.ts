import { Observable } from "rxjs";
import { TaxaCreateViewModel } from "../viewModels/taxa/TaxaCreateViewModel";
import { TaxaListViewModel } from "../viewModels/taxa/TaxaListViewModel";

export interface IHttpTaxaService {

    obterTaxa(): Observable<TaxaListViewModel[]>

    adicionarTaxa(taxa: TaxaCreateViewModel): Observable<TaxaCreateViewModel>

    //obterTaxaPorId(taxaId: number): Observable<CupomDetailsViewModel>

    //editarTaxa(taxa: CupomEditViewModel): Observable<CupomEditViewModel>

   excluirTaxa(taxaId: number): Observable<number>
}