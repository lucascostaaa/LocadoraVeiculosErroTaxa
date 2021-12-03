import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IHttpGrupoVeiculoService } from "src/app/shared/interfaces/IHttpGrupoVeiculoService";
import { GrupoVeiculoCreateViewModel } from "src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoCreateViewModel";
import { GrupoVeiculoDetailsViewModel } from "src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoDetailsViewModel";
import { GrupoVeiculoEditViewModel } from "src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoEditViewModel";
import { GrupoVeiculoListViewModel } from "src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoListViewModel";

@Injectable({
    providedIn: 'root'
})
export class HttpGrupoVeiculoService implements IHttpGrupoVeiculoService {

    private apiUrl = 'http://localhost:32753/api/GrupoVeiculo';

    constructor(private http: HttpClient) { }


    editarGrupoVeiculo(grupoVeiculo: GrupoVeiculoEditViewModel): Observable<GrupoVeiculoEditViewModel> {
        return this.http.put<GrupoVeiculoEditViewModel>(`${this.apiUrl}/${grupoVeiculo.id}`, grupoVeiculo);
    }


    obterGrupoVeiculoPorId(grupoVeiculoId: number): Observable<GrupoVeiculoDetailsViewModel> {
        return this.http.get<GrupoVeiculoDetailsViewModel>(`${this.apiUrl}/${grupoVeiculoId}`);
    }

    public adicionarGrupoVeiculo(grupoVeiculo: GrupoVeiculoCreateViewModel): Observable<GrupoVeiculoCreateViewModel> {
        return this.http.post<GrupoVeiculoCreateViewModel>(this.apiUrl, grupoVeiculo);
    }
    
    public excluirGrupo(grupoVeiculoId: number): Observable<number> {
        return this.http.delete<number>(`${this.apiUrl}/${grupoVeiculoId}`);
    }

    public obterGrupoVeiculo(): Observable<GrupoVeiculoListViewModel[]> {
        return this.http.get<GrupoVeiculoListViewModel[]>(`${this.apiUrl}`);
    }

}