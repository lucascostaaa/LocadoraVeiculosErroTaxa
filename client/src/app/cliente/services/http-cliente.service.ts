import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IHttpClienteService } from "src/app/shared/interfaces/IHttpClienteService";
import { ClienteCreateViewModel } from "src/app/shared/viewModels/cliente/ClienteCreateViewModel";
import { ClienteDetailsViewModel } from "src/app/shared/viewModels/cliente/ClienteDetailsViewModel";
import { ClienteEditViewModel } from "src/app/shared/viewModels/cliente/ClienteEditViewModel";
import { ClienteListViewModel } from "src/app/shared/viewModels/cliente/ClienteListViewModel";

@Injectable({
    providedIn: 'root'
  })
  export class HttpClienteService implements IHttpClienteService {
  
    private apiUrl = 'http://localhost:32753/api/cliente';
  
    constructor(private http: HttpClient) { }
  
    public obterCliente(): Observable<ClienteListViewModel[]> {
      return this.http.get<ClienteListViewModel[]>(`${this.apiUrl}`);
    }
  
    public adicionarCliente(cliente: ClienteCreateViewModel): Observable<ClienteCreateViewModel> {
      return this.http.post<ClienteCreateViewModel>(this.apiUrl, cliente);
    }
  
    public obterClientePorId(clienteId: number): Observable<ClienteDetailsViewModel> {
      return this.http.get<ClienteDetailsViewModel>(`${this.apiUrl}/${clienteId}`);
    }
  
    public editarCliente(cliente: ClienteEditViewModel): Observable<ClienteEditViewModel> {
      return this.http.put<ClienteEditViewModel>(`${this.apiUrl}/${cliente.id}`, cliente);
    }
  
    public excluirCliente(clienteId: number): Observable<number> {
      return this.http.delete<number>(`${this.apiUrl}/${clienteId}`);
    }
  }