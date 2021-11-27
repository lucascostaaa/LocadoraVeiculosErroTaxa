import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IHttpCupomService } from 'src/app/shared/interfaces/IHttpCupomService';
import { CupomCreateViewModel } from 'src/app/shared/viewModels/cupom/CupomCreateViewModel';
import { CupomDetailsViewModel } from 'src/app/shared/viewModels/cupom/CupomDetailsViewModel';
import { CupomEditViewModel } from 'src/app/shared/viewModels/cupom/CupomEditViewModel';
import { CupomListViewModel } from 'src/app/shared/viewModels/cupom/CupomListViewModel';

@Injectable({
  providedIn: 'root'
})
export class HttpCupomService implements IHttpCupomService {

  private apiUrl = 'http://localhost:32753/api/cupom';

  constructor(private http: HttpClient) { }

  public obterCupons(): Observable<CupomListViewModel[]> {
    return this.http.get<CupomListViewModel[]>(`${this.apiUrl}`);
  }

  public adicionarCupom(cupom: CupomCreateViewModel): Observable<CupomCreateViewModel> {
    return this.http.post<CupomCreateViewModel>(this.apiUrl, cupom);
  }

  public obterCupomPorId(cupomId: number): Observable<CupomDetailsViewModel> {
    return this.http.get<CupomDetailsViewModel>(`${this.apiUrl}/${cupomId}`);
  }

  public editarCupom(cupom: CupomEditViewModel): Observable<CupomEditViewModel> {
    return this.http.put<CupomEditViewModel>(`${this.apiUrl}/${cupom.id}`, cupom);
  }

  public excluirCupom(cupomId: number): Observable<number> {
    return this.http.delete<number>(`${this.apiUrl}/${cupomId}`);
  }
}
