import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NumberValueAccessor, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IHttpTaxaService } from 'src/app/shared/interfaces/IHttpTaxaService';
import { EstadoTaxaLocacao } from 'src/app/shared/models/EstadoLocacaoEnum';
import { TaxaType } from 'src/app/shared/models/TaxaEnum';
import { ToastService } from 'src/app/shared/services/toast.service';
import { TaxaCreateViewModel } from 'src/app/shared/viewModels/taxa/TaxaCreateViewModel';

@Component({
  selector: 'app-taxa-criar',
  templateUrl: './taxa-criar.component.html',
})
export class TaxaCriarComponent implements OnInit {

  cadastroForm: FormGroup;

  taxa: TaxaCreateViewModel;

  tipos = TaxaType;
  estadoTaxaLocacao = EstadoTaxaLocacao;
  chaves: any[];


  constructor(@Inject('IHttpTaxaServiceToken') private servicoTaxa: IHttpTaxaService,
    private router: Router,

    private toastService: ToastService) { }

  ngOnInit(): void {
    this.chaves = Object.keys(this.tipos).filter(t => !isNaN(Number(t)));
    this.chaves = Object.keys(this.estadoTaxaLocacao).filter(t => !isNaN(Number(t)));

    this.cadastroForm = new FormGroup({
      nome: new FormControl('', Validators.required),
      valor: new FormControl('', Validators.compose([Validators.required, Validators.min(1)])),
      tipo: new FormControl(''),
      estadoLocacao: new FormControl('')
    });

  }
  adicionarTaxa() {
    this.cadastroForm.valid
    this.taxa = Object.assign({}, this.taxa, this.cadastroForm.value);

    this.servicoTaxa.adicionarTaxa(this.taxa)
      .subscribe(
        taxa => {
          this.toastService.show('Taxa ' + taxa.nome + ' adicionado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['taxa/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Ja a um taxa cadastrado com esse nome: ' + erro.error.errors["Nome"],
            { classname: 'bg-danger text-light', delay: 5000 });

        }
      );

  }
  cancelar
  (): void {
    this.router.navigate(['taxa/listar']);
  }

}
