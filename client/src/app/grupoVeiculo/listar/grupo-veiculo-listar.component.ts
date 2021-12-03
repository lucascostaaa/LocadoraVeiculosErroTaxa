import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IHttpGrupoVeiculoService } from 'src/app/shared/interfaces/IHttpGrupoVeiculoService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { GrupoVeiculoListViewModel } from 'src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoListViewModel';

@Component({
  selector: 'app-grupo-veiculo-listar',
  templateUrl: './grupo-veiculo-listar.component.html',
})
export class GrupoVeiculoListarComponent implements OnInit {

  listaGrupoVeiculoTotal: GrupoVeiculoListViewModel[];
  listaGrupoVeiculo: GrupoVeiculoListViewModel[];
  grupoVeiculoSelecionado: any;

  page = 1;
  pageSize = 5;
  collectionSize = 0;


  constructor(private router: Router, @Inject('IHttpGrupoVeiculoServiceToken') private servicoGrupoVeiculo: IHttpGrupoVeiculoService, private servicoModal: NgbModal, private toastService: ToastService) { }

  ngOnInit(): void {
    this.ObterGrupoVeiculo();
  }

  ObterGrupoVeiculo(): void {
    this.servicoGrupoVeiculo.obterGrupoVeiculo()
      .subscribe(parceiros => {
        this.listaGrupoVeiculoTotal = parceiros;
        this.atualizarGrupoVeiculo();
      });
  }

  atualizarGrupoVeiculo() {
    this.listaGrupoVeiculo = this.listaGrupoVeiculoTotal
      .map((parceiro, i) => ({ u: i + 1, ...parceiro }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);

    this.collectionSize = this.listaGrupoVeiculoTotal.length;
  }

  abrirConfirmacao(modal: any) {
    this.servicoModal.open(modal).result.then((resultado) => {
      if (resultado == 'Excluir') {
        this.servicoGrupoVeiculo.excluirGrupo(this.grupoVeiculoSelecionado)
        .subscribe(
          () => {
            this.toastService.show('Grupo de Veiculo removido com sucesso!',
              { classname: 'bg-success text-light', delay: 5000 });

            setTimeout(() => {
              this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
                this.router.navigate(['grupoVeiculo/listar']);
              });
            }, 5000);
          },
          erro => {
            this.toastService.show('Erro ao remover parceiro: ' + erro.error.errors["Nome"].toString(),
              { classname: 'bg-danger text-light', delay: 5000 });
          }
        );
      }
    }).catch(erro => erro);
  }

}
