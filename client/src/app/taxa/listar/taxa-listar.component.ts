import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IHttpTaxaService } from 'src/app/shared/interfaces/IHttpTaxaService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { TaxaListViewModel } from 'src/app/shared/viewModels/taxa/TaxaListViewModel';

@Component({
  selector: 'app-taxa-listar',
  templateUrl: './taxa-listar.component.html',
})
export class TaxaListarComponent implements OnInit {

  listaTaxas: TaxaListViewModel[];
  listaTaxasTotal: TaxaListViewModel[];
  taxaSelecionada: any;

  page = 1;
  pageSize = 5;
  collectionSize = 0;

  constructor(private router: Router,
    @Inject('IHttpTaxaServiceToken') private servicoTaxa: IHttpTaxaService, private servicoModal: NgbModal, private toastService: ToastService) { }

  ngOnInit(): void {
    this.obterTaxa();
  }

  obterTaxa(): void {
    this.servicoTaxa.obterTaxa()
      .subscribe(taxas => {
        this.listaTaxasTotal = taxas;
        this.atualizarTaxas();
      });
  }

  atualizarTaxas() {
    this.listaTaxas = this.listaTaxasTotal
      .map((taxa, i) => ({ u: i + 1, ...taxa }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);

    this.collectionSize = this.listaTaxasTotal.length;
  }

  abrirConfirmacao(modal: any) {
    this.servicoModal.open(modal).result.then((resultado) => {
      if (resultado == 'Excluir') {
        this.servicoTaxa.excluirTaxa(this.taxaSelecionada)
        .subscribe(
          () => {
            this.toastService.show('Taxa removido com sucesso!',
              { classname: 'bg-success text-light', delay: 5000 });

            setTimeout(() => {
              this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
                this.router.navigate(['taxa/listar']);
              });
            }, 5000);
          },
          erro => {
            this.toastService.show('Erro ao remover taxa: ' + erro.error.errors["Nome"],
              { classname: 'bg-danger text-light', delay: 5000 });
          }
        );
      }
    }).catch(erro => erro);
  }


}
