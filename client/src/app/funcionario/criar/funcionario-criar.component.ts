import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { IHttpFuncionarioService } from 'src/app/shared/interfaces/IHttpFuncionarioService';
import { ToastService } from 'src/app/shared/services/toast.service';
import { valorMinimo } from 'src/app/shared/validators/valor-cupom.directive';
import { FunicionarioCreateViewModel } from 'src/app/shared/viewModels/funcionario/FuncionarioCreateViewModel';

@Component({
  selector: 'app-funcionario-criar',
  templateUrl: './funcionario-criar.component.html',
})
export class FuncionarioCriarComponent implements OnInit {

  cadastroForm: FormGroup;
  dataAdmissao: NgbDateStruct;

  funcionario: FunicionarioCreateViewModel;
  constructor(@Inject('IHttpFuncionarioServiceToken') private servicoFuncionario: IHttpFuncionarioService, private router: Router, private toastService: ToastService) { }

  ngOnInit(): void {
    this.cadastroForm = new FormGroup({
      nome: new FormControl('', Validators.required),
      usuario: new FormControl('', Validators.compose([Validators.required])),
      senha: new FormControl('', Validators.required),
      dataAdmissao: new FormControl('', Validators.required),
      salario: new FormControl('', Validators.compose([Validators.required, Validators.min(1)]))
    });
  }

  adicionarFuncionario() {
    this.cadastroForm.valid;
    this.funcionario = Object.assign({}, this.funcionario, this.cadastroForm.value);

    this.servicoFuncionario.adicionarFuncionario(this.funcionario)
      .subscribe(
        funcionario => {
          this.toastService.show('Funcionario ' + funcionario.nome + ' adicionado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['funcionario/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Usuario ja cadastrado!: ' + erro.error.errors["Usuario"],
            { classname: 'bg-danger text-light', delay: 5000 });
        }
      );
  }

  
  cancelar(): void {
    this.router.navigate(['funcionario/listar']);
  }

}
