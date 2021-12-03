import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IHttpClienteService } from 'src/app/shared/interfaces/IHttpClienteService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { ClienteListViewModel } from 'src/app/shared/viewModels/cliente/ClienteListViewModel';

@Component({
  selector: 'app-cliente-listar',
  templateUrl: './cliente-listar.component.html',
})
export class ClienteListarComponent implements OnInit {

  listaCleinte: ClienteListViewModel[];
  listaCleinteTotal: ClienteListViewModel[];
  clienteSelecionado: any;

  page = 1;
  pageSize = 5;
  collectionSize = 0;

  constructor(private router: Router,
    @Inject('IHttpClienteServiceToken') private servicoCliente: IHttpClienteService, private servicoModal: NgbModal, private toastService: ToastService) { }


  ngOnInit(): void {
    this.obterClientes();

  }

  obterClientes(): void {
    this.servicoCliente.obterCliente()
      .subscribe(clientes => {
        this.listaCleinteTotal = clientes;
        this.atualizarCliente();
      });
  }

  atualizarCliente() {
    this.listaCleinte = this.listaCleinteTotal
      .map((cliente, i) => ({ u: i + 1, ...cliente }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);

    this.collectionSize = this.listaCleinteTotal.length;
  }

  abrirConfirmacao(modal: any) {
    this.servicoModal.open(modal).result.then((resultado) => {
      if (resultado == 'Excluir') {
        this.servicoCliente.excluirCliente(this.clienteSelecionado)
        .subscribe(
          () => {
            this.toastService.show('Cliente removido com sucesso!',
              { classname: 'bg-success text-light', delay: 5000 });

            setTimeout(() => {
              this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
                this.router.navigate(['cleinte/listar']);
              });
            }, 5000);
          },
          erro => {
            this.toastService.show('Erro ao remover cleinte: ' + erro.error.errors["Nome"].toString(),
              { classname: 'bg-danger text-light', delay: 5000 });
          }
        );
      }
    }).catch(erro => erro);
  }
}
