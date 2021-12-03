import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './navegacao/footer/footer.component';
import { HeaderComponent } from './navegacao/header/header.component';
import { MenuComponent } from './navegacao/menu/menu.component';
import { HomeComponent } from './home/home.component';
import { ParceiroListarComponent } from './parceiro/listar/parceiro-listar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ParceiroCriarComponent } from './parceiro/criar/parceiro-criar.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ParceiroEditarComponent } from './parceiro/editar/parceiro-editar.component';
import { CupomListarComponent } from './cupom/listar/cupom-listar.component';
import { CupomCriarComponent } from './cupom/criar/cupom-criar.component';
import { CupomEditarComponent } from './cupom/editar/cupom-editar.component';
import { HttpParceiroService } from './parceiro/services/http-parceiro.service';
import { HttpCupomService } from './cupom/services/http-cupom.service';
import { FuncionarioCriarComponent } from './funcionario/criar/funcionario-criar.component';
import { HttpFuncionarioService } from './funcionario/services/http-funcionario.service';
import { FuncionarioListarComponent } from './funcionario/listar/funcionario-listar.component';
import { FuncionarioEditarComponent } from './funcionario/editar/funcionario-editar.component';
import { ToastContainerComponent } from './shared/components/toast-container/toast-container.component';
import { FormatarEnumPipe } from './shared/pipes/formatar-enum.pipe';
import { TaxaListarComponent } from './taxa/listar/taxa-listar.component';
import { HttpTaxaService } from './taxa/listar/services/http-taxa.service';
import { TaxaCriarComponent } from './taxa/criar/taxa-criar.component';
import { TaxaEditarComponent } from './taxa/editar/taxa-editar.component';
import { GrupoVeiculoCriarComponent } from './grupoVeiculo/criar/grupo-veiculo-criar.component';
import { GrupoVeiculoListarComponent } from './grupoVeiculo/listar/grupo-veiculo-listar.component';
import { HttpGrupoVeiculoService } from './grupoVeiculo/services/http-grupoVeiculo.service';
import { GrupoVeiculoEditarComponent } from './grupoVeiculo/editar/grupo-veiculo-editar.component';
import { ClienteListarComponent } from './cliente/listar/cliente-listar.component';
import { HttpClienteService } from './cliente/services/http-cliente.service';
import { ClienteCriarComponent } from './cliente/criar/cliente-criar.component';
import { ClienteEditarComponent } from './cliente/editar/cliente-editar.component';


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    MenuComponent,
    HomeComponent,
    ParceiroListarComponent,
    ParceiroCriarComponent,
    ParceiroEditarComponent,
    CupomListarComponent,
    CupomCriarComponent,
    CupomEditarComponent,
    FuncionarioCriarComponent,
    FuncionarioListarComponent,
    FuncionarioEditarComponent,
    ToastContainerComponent,
    FormatarEnumPipe,
    TaxaListarComponent,
    TaxaCriarComponent,
    TaxaEditarComponent,
    GrupoVeiculoCriarComponent,
    GrupoVeiculoListarComponent,
    GrupoVeiculoEditarComponent,
    ClienteListarComponent,
    ClienteCriarComponent,
    ClienteEditarComponent,
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    NgbModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'en' },
    { provide: 'IHttpParceiroServiceToken', useClass: HttpParceiroService },
    { provide: 'IHttpCupomServiceToken', useClass: HttpCupomService },
    { provide: 'IHttpFuncionarioServiceToken', useClass: HttpFuncionarioService },
    { provide: 'IHttpTaxaServiceToken', useClass: HttpTaxaService },
    { provide: 'IHttpGrupoVeiculoServiceToken', useClass: HttpGrupoVeiculoService },
    { provide: 'IHttpClienteServiceToken', useClass: HttpClienteService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
