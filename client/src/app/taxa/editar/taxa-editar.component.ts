import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IHttpTaxaService } from 'src/app/shared/interfaces/IHttpTaxaService';
import { EstadoTaxaLocacao } from 'src/app/shared/models/EstadoLocacaoEnum';
import { TaxaType } from 'src/app/shared/models/TaxaEnum';
import { ToastService } from 'src/app/shared/services/toast.service';
import { TaxaDetailsViewModel } from 'src/app/shared/viewModels/taxa/TaxaDetailsViewModel';
import { TaxaEditViewModel } from 'src/app/shared/viewModels/taxa/TaxaEditViewModel';

@Component({
  selector: 'app-taxa-editar',
  templateUrl: './taxa-editar.component.html',
})
export class TaxaEditarComponent implements OnInit {

  cadastroForm: FormGroup;
  id: any;
  taxa: TaxaEditViewModel;

  tipoTaxa = TaxaType;
  estadoLocacao = EstadoTaxaLocacao;
  chaves: any[];

  constructor(private _Activatedroute: ActivatedRoute,
    @Inject('IHttpTaxaServiceToken') private servicoTaxa: IHttpTaxaService,
    private router: Router,
    private toastService: ToastService) { }

  ngOnInit(): void {

    this.id = this._Activatedroute.snapshot.paramMap.get("id");

    this.chaves = Object.keys(this.tipoTaxa).filter(t => !isNaN(Number(t)));
    this.chaves = Object.keys(this.estadoLocacao).filter(t => !isNaN(Number(t)));

    this.cadastroForm = new FormGroup({
      nome: new FormControl(''),
      valor: new FormControl(''),
      tipoTaxa: new FormControl(''),
      estadoLocacao: new FormControl('')

    });

    this.carregarTaxa();

  }
  carregarTaxa(): void {
    this.servicoTaxa.obterTaxaPorId(this.id)
      .subscribe((taxa: TaxaDetailsViewModel) => {
        this.carregarFormulario(taxa);
      });
  }

  carregarFormulario(taxa: TaxaDetailsViewModel) {
    this.cadastroForm = new FormGroup({
      nome: new FormControl(taxa.nome, Validators.required),
      valor: new FormControl(taxa.valor, Validators.compose([Validators.required, Validators.min(1)])),
      tipoTaxa: new FormControl(taxa.tipoTaxa),
      estadoLocacao: new FormControl(taxa.estadoLocacao)

    })
  }

  atualizarTaxa() {
    this.cadastroForm.valid
    this.taxa = Object.assign({}, this.taxa, this.cadastroForm.value);
    this.taxa.id = this.id;

    this.servicoTaxa.editarTaxa(this.taxa)
      .subscribe(
        taxa => {
          this.toastService.show('Taxa ' + taxa.nome + ' editado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['taxa/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Erro ao editar taxa: ' + erro.error.errors["Nome"].ToString(),
            { classname: 'bg-danger text-light', delay: 5000 });
        });
  }

  
  cancelar(): void {
    this.router.navigate(['taxa/listar']);
  }
}


