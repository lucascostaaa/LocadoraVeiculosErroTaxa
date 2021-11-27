import { Observable } from "rxjs";
import { FuncionarioEditarComponent } from "src/app/funcionario/editar/funcionario-editar.component";
import { FunicionarioCreateViewModel } from "../viewModels/funcionario/FuncionarioCreateViewModel";
import { FuncionarioDetailsViewModel } from "../viewModels/funcionario/FuncionarioDetailsViewModel";
import { FuncionarioEditViewModel } from "../viewModels/funcionario/FuncionarioEditViewModel";
import { FuncionarioListViewModel } from "../viewModels/funcionario/FuncionarioListViewModel";

export interface IHttpFuncionarioService {

    obterFuncionario(): Observable<FuncionarioListViewModel[]>
    
    adicionarFuncionario(funcionario: FunicionarioCreateViewModel): Observable<FunicionarioCreateViewModel>

    excluirFuncionario(funcionarioId: number): Observable<number>

    obterFuncionarioPorId(funcionarioId: number):Observable<FuncionarioDetailsViewModel>

    editarFuncionario(funcionario: FuncionarioEditViewModel): Observable<FuncionarioEditViewModel>

}