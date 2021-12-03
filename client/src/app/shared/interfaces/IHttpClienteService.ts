import { Observable } from "rxjs";
import { ClienteCreateViewModel } from "../viewModels/cliente/ClienteCreateViewModel";
import { ClienteDetailsViewModel } from "../viewModels/cliente/ClienteDetailsViewModel";
import { ClienteEditViewModel } from "../viewModels/cliente/ClienteEditViewModel";
import { ClienteListViewModel } from "../viewModels/cliente/ClienteListViewModel";

export interface IHttpClienteService {

    obterCliente(): Observable<ClienteListViewModel[]>

    adicionarCliente(cliente: ClienteCreateViewModel): Observable<ClienteCreateViewModel>

    obterClientePorId(clienteId: number): Observable<ClienteDetailsViewModel>

    editarCliente(cliente: ClienteEditViewModel): Observable<ClienteEditViewModel>

    excluirCliente(clienteId: number): Observable<number>
}