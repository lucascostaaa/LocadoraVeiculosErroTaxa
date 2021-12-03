import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteCriarComponent } from './cliente/criar/cliente-criar.component';
import { ClienteEditarComponent } from './cliente/editar/cliente-editar.component';
import { ClienteListarComponent } from './cliente/listar/cliente-listar.component';
import { CupomCriarComponent } from './cupom/criar/cupom-criar.component';
import { CupomEditarComponent } from './cupom/editar/cupom-editar.component';
import { CupomListarComponent } from './cupom/listar/cupom-listar.component';
import { FuncionarioCriarComponent } from './funcionario/criar/funcionario-criar.component';
import { FuncionarioEditarComponent } from './funcionario/editar/funcionario-editar.component';
import { FuncionarioListarComponent } from './funcionario/listar/funcionario-listar.component';
import { GrupoVeiculoCriarComponent } from './grupoVeiculo/criar/grupo-veiculo-criar.component';
import { GrupoVeiculoEditarComponent } from './grupoVeiculo/editar/grupo-veiculo-editar.component';
import { GrupoVeiculoListarComponent } from './grupoVeiculo/listar/grupo-veiculo-listar.component';
import { HomeComponent } from './home/home.component';
import { ParceiroCriarComponent } from './parceiro/criar/parceiro-criar.component';
import { ParceiroEditarComponent } from './parceiro/editar/parceiro-editar.component';
import { ParceiroListarComponent } from './parceiro/listar/parceiro-listar.component';
import { TaxaCriarComponent } from './taxa/criar/taxa-criar.component';
import { TaxaEditarComponent } from './taxa/editar/taxa-editar.component';
import { TaxaListarComponent } from './taxa/listar/taxa-listar.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'parceiro/listar', component: ParceiroListarComponent },
  { path: 'parceiro/criar', component: ParceiroCriarComponent },
  { path: 'parceiro/editar/:id', component: ParceiroEditarComponent },
  { path: 'cupom/listar', component: CupomListarComponent },
  { path: 'cupom/criar', component: CupomCriarComponent },
  { path: 'cupom/editar/:id', component: CupomEditarComponent },
  { path: 'funcionario/listar', component: FuncionarioListarComponent },
  { path: 'funcionario/criar', component: FuncionarioCriarComponent},
  { path: 'funcionario/editar/:id', component: FuncionarioEditarComponent},
  { path: 'taxa/listar', component: TaxaListarComponent},
  { path: 'taxa/criar', component: TaxaCriarComponent},
  { path: 'taxa/editar/:id', component: TaxaEditarComponent},
  { path: 'grupoVeiculo/listar', component: GrupoVeiculoListarComponent},
  { path: 'grupoVeiculo/criar', component: GrupoVeiculoCriarComponent},
  { path: 'grupoVeiculo/editar/:id', component: GrupoVeiculoEditarComponent},
  { path: 'cliente/listar', component: ClienteListarComponent},
  { path: 'cliente/criar', component: ClienteCriarComponent},
  { path: 'cliente/editar/:id', component: ClienteEditarComponent},



  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
