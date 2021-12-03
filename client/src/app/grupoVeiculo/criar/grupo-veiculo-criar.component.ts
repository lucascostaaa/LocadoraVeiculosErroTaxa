import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IHttpGrupoVeiculoService } from 'src/app/shared/interfaces/IHttpGrupoVeiculoService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { GrupoVeiculoCreateViewModel } from 'src/app/shared/viewModels/grupoVeiculo/GrupoVeiculoCreateViewModel';

@Component({
  selector: 'app-grupo-veiculo-criar',
  templateUrl: './grupo-veiculo-criar.component.html',
})
export class GrupoVeiculoCriarComponent implements OnInit {

  cadastroForm: FormGroup;
  grupoVeiculo: GrupoVeiculoCreateViewModel;

  constructor(@Inject('IHttpGrupoVeiculoServiceToken') private servicoGrupoVeiculo: IHttpGrupoVeiculoService, private router: Router, private toastService: ToastService) { }

  ngOnInit(): void {
    this.cadastroForm = new FormGroup({
      nome: new FormControl('', Validators.required)
    });
  }

  adicionarGrupoVeiculo() {
    this.cadastroForm.valid;
    this.grupoVeiculo = Object.assign({}, this.grupoVeiculo, this.cadastroForm.value);

    this.servicoGrupoVeiculo.adicionarGrupoVeiculo(this.grupoVeiculo)
      .subscribe(
        parceiro => {
          this.toastService.show('Grupo Veiculo ' + parceiro.nome + ' adicionado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['grupoVeiculo/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Grupo Veiculo ja cadastrado com esse nome! ' + erro.error.errors["Nome"],
            { classname: 'bg-danger text-light', delay: 5000 });
        }
      );
  }

  cancelar(): void {
    this.router.navigate(['grupoVeiculo/listar']);
  }

}
