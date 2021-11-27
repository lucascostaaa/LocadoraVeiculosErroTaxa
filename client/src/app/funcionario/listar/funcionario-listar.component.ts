import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IHttpFuncionarioService } from 'src/app/shared/interfaces/IHttpFuncionarioService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { FuncionarioListViewModel} from 'src/app/shared/viewModels/funcionario/FuncionarioListViewModel';
import { HttpFuncionarioService } from '../services/http-funcionario.service';

@Component({
  selector: 'app-funcionario-listar',
  templateUrl: './funcionario-listar.component.html',
})
export class FuncionarioListarComponent implements OnInit {

  listaFuncionarios: FuncionarioListViewModel[];
  listaFuncionariosTotal: FuncionarioListViewModel[];
  funcionarioelecionado: any;

  page = 1;
  pageSize = 5;
  collectionSize = 0;
  constructor(private router: Router, @Inject('IHttpFuncionarioServiceToken') private servicoFuncionario: IHttpFuncionarioService, private servicoModal: NgbModal, private toastService: ToastService) { }

  ngOnInit(): void {
    this.obterFuncionario();
  }
  obterFuncionario(): void {
    this.servicoFuncionario.obterFuncionario()
      .subscribe(funcionarios => {
        this.listaFuncionariosTotal = funcionarios;
        this.atualizarFuncionarios();
      });
  }
  atualizarFuncionarios() {
    this.listaFuncionarios = this.listaFuncionariosTotal
      .map((funcionario, i) => ({ u: i + 1, ...funcionario }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);

    this.collectionSize = this.listaFuncionariosTotal.length;
  }

  formatarData(data: Date): string {
    return new Date(data).toLocaleDateString();
  }

  abrirConfirmacao(modal: any) {
    this.servicoModal.open(modal).result.then((resultado) => {
      if (resultado == 'Excluir') {
        this.servicoFuncionario.excluirFuncionario(this.funcionarioelecionado)
        .subscribe(
          () => {
            this.toastService.show('Funcionario removido com sucesso!',
              { classname: 'bg-success text-light', delay: 5000 });

            setTimeout(() => {
              this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
                this.router.navigate(['funcionario/listar']);
              });
            }, 5000);
          },
          erro => {
            this.toastService.show('Erro ao remover funcionario: ' + erro.error.errors["Nome"].toString(),
              { classname: 'bg-danger text-light', delay: 5000 });
          }
        );
      }
    }).catch(erro => erro);
  }
}
