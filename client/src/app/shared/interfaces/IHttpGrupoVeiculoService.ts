import { Observable } from "rxjs";
import { GrupoVeiculoListarComponent } from "src/app/grupoVeiculo/listar/grupo-veiculo-listar.component";
import { GrupoVeiculoCreateViewModel } from "../viewModels/grupoVeiculo/GrupoVeiculoCreateViewModel";
import { GrupoVeiculoDetailsViewModel } from "../viewModels/grupoVeiculo/GrupoVeiculoDetailsViewModel";
import { GrupoVeiculoEditViewModel } from "../viewModels/grupoVeiculo/GrupoVeiculoEditViewModel";
import { GrupoVeiculoListViewModel } from "../viewModels/grupoVeiculo/GrupoVeiculoListViewModel";

export interface IHttpGrupoVeiculoService{


    obterGrupoVeiculo(): Observable<GrupoVeiculoListViewModel[]>

    excluirGrupo(grupoVeiculoId: number): Observable<number>

    adicionarGrupoVeiculo(parceiro: GrupoVeiculoCreateViewModel): Observable<GrupoVeiculoCreateViewModel>

    obterGrupoVeiculoPorId(parceiroId: number): Observable<GrupoVeiculoDetailsViewModel>

    editarGrupoVeiculo(parceiro: GrupoVeiculoEditViewModel): Observable<GrupoVeiculoEditViewModel>
}