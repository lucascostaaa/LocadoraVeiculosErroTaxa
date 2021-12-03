import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IHttpGrupoVeiculoService } from 'src/app/shared/interfaces/IHttpGrupoVeiculoService';
import { GrupoVeiculo } from 'src/app/shared/models/GrupoVeiculo';
import { ToastService } from 'src/app/shared/services/toast.service';
import { GrupoVeiculoDetailsViewModel } from 'src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoDetailsViewModel';
import { GrupoVeiculoEditViewModel } from 'src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoEditViewModel';
import { GrupoVeiculoCriarComponent } from '../criar/grupo-veiculo-criar.component';

@Component({
  selector: 'app-grupo-veiculo-editar',
  templateUrl: './grupo-veiculo-editar.component.html',
})
export class GrupoVeiculoEditarComponent implements OnInit {

  sub: any;
  id: any;
  grupoVeiculo: GrupoVeiculoEditViewModel;
  cadastroForm : FormGroup;



  constructor(private _Activatedroute: ActivatedRoute, @Inject('IHttpGrupoVeiculoServiceToken') private servicoGrupoVeiculo: IHttpGrupoVeiculoService, private router: Router, private toastService: ToastService) { }

  ngOnInit(): void {
    this.id = this._Activatedroute.snapshot.paramMap.get("id");

    this.cadastroForm = new FormGroup({
      id: new FormControl(''),
      nome: new FormControl('')
    });

    this.carregarGrupoVeiculos();
  }


  carregarGrupoVeiculos(){
    this.servicoGrupoVeiculo.obterGrupoVeiculoPorId(this.id)
    .subscribe((grupoVeiculo: GrupoVeiculoDetailsViewModel) => {
      this.carregarFormulario(grupoVeiculo);
    });
  }

  atualizarGrupoVeiculo() {
    this.cadastroForm.valid;
    this.grupoVeiculo = Object.assign({}, this.grupoVeiculo, this.cadastroForm.value);
    this.grupoVeiculo.id = this.id;

    this.servicoGrupoVeiculo.editarGrupoVeiculo(this.grupoVeiculo)
    .subscribe(
      grupoVeiculo => {
        this.toastService.show('Grupo Veiculo'  + grupoVeiculo.nome + ' editado com sucesso',
          { classname: 'bg-success text-light', delay: 5000 });
        setTimeout(() => {
          this.router.navigate(['grupoVeiculo/listar']);
        }, 5000);
      },
      erro => {
        this.toastService.show('Erro ao editar Grupo Veiculo: ' + erro.error.errors["Nome"],
          { classname: 'bg-danger text-light', delay: 5000 });
      });
  }

  carregarFormulario(grupoVeiculo: GrupoVeiculoDetailsViewModel) {

    this.cadastroForm = new FormGroup({
      id: new FormControl(grupoVeiculo.id),
      nome: new FormControl(grupoVeiculo.nome, Validators.required),
    });
  }

  cancelar(): void {
    this.router.navigate(['grupoVeiculo/listar']);
  }

}
