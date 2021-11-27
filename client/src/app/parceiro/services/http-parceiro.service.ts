import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IHttpParceiroService } from 'src/app/shared/interfaces/IHttpParceiroService';
import { ParceiroCreateViewModel } from 'src/app/shared/viewModels/parceiro/ParceiroCreateViewModel';
import { ParceiroDetailsViewModel } from 'src/app/shared/viewModels/parceiro/ParceiroDetailsViewModel';
import { ParceiroEditViewModel } from 'src/app/shared/viewModels/parceiro/ParceiroEditViewModel';
import { ParceiroListViewModel } from 'src/app/shared/viewModels/parceiro/ParceiroListViewModel';

@Injectable({
    providedIn: 'root'
})
export class HttpParceiroService implements IHttpParceiroService {

    private apiUrl = 'http://localhost:32753/api/parceiro';

    constructor(private http: HttpClient) { }

    public obterParceiros(): Observable<ParceiroListViewModel[]> {
        return this.http.get<ParceiroListViewModel[]>(`${this.apiUrl}`);
    }

    public adicionarParceiro(parceiro: ParceiroCreateViewModel): Observable<ParceiroCreateViewModel> {
        return this.http.post<ParceiroCreateViewModel>(this.apiUrl, parceiro);
    }

    public obterParceiroPorId(parceiroId: number): Observable<ParceiroDetailsViewModel> {
        return this.http.get<ParceiroDetailsViewModel>(`${this.apiUrl}/${parceiroId}`);
    }

    public editarParceiro(parceiro: ParceiroEditViewModel): Observable<ParceiroEditViewModel> {
        return this.http.put<ParceiroEditViewModel>(`${this.apiUrl}/${parceiro.id}`, parceiro);
    }

    public excluirParceiro(parceiroId: number): Observable<number> {
        return this.http.delete<number>(`${this.apiUrl}/${parceiroId}`);
    }
}