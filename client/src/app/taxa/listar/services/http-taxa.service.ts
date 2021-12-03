import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IHttpTaxaService } from "src/app/shared/interfaces/IHttpTaxaService";
import { Taxa } from "src/app/shared/models/Taxa";
import { TaxaCreateViewModel } from "src/app/shared/viewModels/taxa/TaxaCreateViewModel";
import { TaxaDetailsViewModel } from "src/app/shared/viewModels/taxa/TaxaDetailsViewModel";
import { TaxaEditViewModel } from "src/app/shared/viewModels/taxa/TaxaEditViewModel";
import { TaxaListViewModel } from "src/app/shared/viewModels/taxa/TaxaListViewModel";
import { TaxaEditarComponent } from "../../editar/taxa-editar.component";

@Injectable({
  providedIn: 'root'

})

export class HttpTaxaService implements IHttpTaxaService {

  private apiUrl = 'http://localhost:32753/api/taxa';

  constructor(private http: HttpClient) { }


  adicionarTaxa(taxa: TaxaCreateViewModel): Observable<TaxaCreateViewModel> {
    return this.http.post<TaxaCreateViewModel>(this.apiUrl, taxa);
  }

  obterTaxa(): Observable<TaxaListViewModel[]> {
    return this.http.get<TaxaListViewModel[]>(`${this.apiUrl}`);

  }
  excluirTaxa(taxaId: number): Observable<number> {
    return this.http.delete<number>(`${this.apiUrl}/${taxaId}`);
  }

  obterTaxaPorId(taxaId: number): Observable<TaxaDetailsViewModel> {
    return this.http.get<TaxaDetailsViewModel>(`${this.apiUrl}/${taxaId}`);
  }
  editarTaxa(taxa: TaxaEditViewModel): Observable<TaxaEditViewModel> {
    return this.http.put<TaxaEditViewModel>(`${this.apiUrl}/${taxa.id}`, taxa);
  }


}
