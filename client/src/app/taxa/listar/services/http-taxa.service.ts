import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IHttpTaxaService } from "src/app/shared/interfaces/IHttpTaxaService";
import { TaxaCreateViewModel } from "src/app/shared/viewModels/taxa/TaxaCreateViewModel";
import { TaxaListViewModel } from "src/app/shared/viewModels/taxa/TaxaListViewModel";

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

  /*   obterFuncionarioPorId(funcionarioId: number): Observable<FuncionarioDetailsViewModel> {
       return this.http.get<FuncionarioDetailsViewModel>(`${this.apiUrl}/${funcionarioId}`);
     }
     editarFuncionario(funcionario: FuncionarioEditViewModel): Observable<FuncionarioEditViewModel> {
       return this.http.put<FuncionarioEditViewModel>(`${this.apiUrl}/${funcionario.id}`, funcionario);
     }
   
 
     }*/

  /* public adicionarFuncionario(funcionario: FunicionarioCreateViewModel): Observable<FunicionarioCreateViewModel> {
     return this.http.post<FunicionarioCreateViewModel>(this.apiUrl, funcionario);
   }
 */

}