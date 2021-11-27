import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IHttpFuncionarioService } from 'src/app/shared/interfaces/IHttpFuncionarioService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { FuncionarioDetailsViewModel } from 'src/app/shared/viewModels/funcionario/FuncionarioDetailsViewModel';
import { FuncionarioEditViewModel } from 'src/app/shared/viewModels/funcionario/FuncionarioEditViewModel';
import { FuncionarioListViewModel } from 'src/app/shared/viewModels/funcionario/FuncionarioListViewModel';

@Component({
  selector: 'app-funcionario-editar',
  templateUrl: './funcionario-editar.component.html',
})
export class FuncionarioEditarComponent implements OnInit {

  cadastroForm: FormGroup;
  id: any;
  funcionario: FuncionarioEditViewModel;
  listaFuncionarios: FuncionarioListViewModel[];

  constructor(private _Activatedroute: ActivatedRoute, @Inject('IHttpFuncionarioServiceToken') private servicoFuncionario: IHttpFuncionarioService, private router: Router, private toastService: ToastService) { }

  ngOnInit(): void {
    this.id = this._Activatedroute.snapshot.paramMap.get("id");

    this.cadastroForm = new FormGroup({
      nome: new FormControl(''),
      usuario: new FormControl(''),
      senha: new FormControl(''),
      dataAdmissao: new FormControl(''),
      salario: new FormControl('')


    });

    this.carregarFuncionarios();

  }

  carregarFuncionarios(): void {
    this.servicoFuncionario.obterFuncionarioPorId(this.id)
      .subscribe((funcionario: FuncionarioDetailsViewModel) => {
        this.carregarFormulario(funcionario);
      });
  }

  carregarFormulario(funcionario: FuncionarioDetailsViewModel) {

    this.cadastroForm = new FormGroup({
      nome: new FormControl(funcionario.nome, Validators.required),
      usuario: new FormControl(funcionario.usuario, Validators.compose([Validators.required])),
      senha: new FormControl(funcionario.senha, Validators.required),
      dataAdmissao: new FormControl(funcionario.dataAdmissao.toLocaleString().substring(0, 10), Validators.required),
      salario: new FormControl(funcionario.salario, Validators.compose([Validators.required, Validators.min(1)]))
    });
  }

  atualizarFuncionario() {
    this.cadastroForm.valid;
    this.funcionario = Object.assign({}, this.funcionario, this.cadastroForm.value);
    this.funcionario.id = this.id;

    this.servicoFuncionario.editarFuncionario(this.funcionario)
      .subscribe(
        funcionario => {
          this.toastService.show('Funcionario ' + funcionario.nome + ' editado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['cupom/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Erro ao editar funcionario: ' + erro.error.errors["Nome"].toString(),
            { classname: 'bg-danger text-light', delay: 5000 });
        });
  }

  cancelar(): void {
    this.router.navigate(['funcionario/listar']);
  }
}
