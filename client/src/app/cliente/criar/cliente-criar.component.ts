import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IHttpClienteService } from 'src/app/shared/interfaces/IHttpClienteService';
import { ClienteType } from 'src/app/shared/models/ClienteEnum';
import { ToastService } from 'src/app/shared/services/toast.service';
import { ClienteCreateViewModel } from 'src/app/shared/viewModels/cliente/ClienteCreateViewModel';

@Component({
  selector: 'app-cliente-criar',
  templateUrl: './cliente-criar.component.html',
})
export class ClienteCriarComponent implements OnInit {

  cadastroForm: FormGroup;

  cliente: ClienteCreateViewModel;

  tipos = ClienteType;
  chaves: any[];

  constructor(@Inject('IHttpClienteServiceToken') private servicoCliente: IHttpClienteService,
  private router: Router,
  private toastService: ToastService) { }

  ngOnInit(): void {
    this.chaves = Object.keys(this.tipos).filter(t => !isNaN(Number(t)));

    this.cadastroForm = new FormGroup({
      nome: new FormControl('', Validators.required),
      endereco: new FormControl('',Validators.required),
      telefone: new FormControl('',Validators.required),
      rg: new FormControl('',Validators.required),
      cpf: new FormControl('',Validators.required),
      cnpj: new FormControl('',Validators.required),
      email: new FormControl('',Validators.required),
      tipo: new FormControl('')
    });

  }


  adicionarCliente() {
    this.cadastroForm.valid
    this.cliente = Object.assign({}, this.cliente, this.cadastroForm.value);

    this.servicoCliente.adicionarCliente(this.cliente)
      .subscribe(
        cliente => {
          this.toastService.show('Cliente ' + cliente.nome + ' adicionado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['cliente/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Ja a um cliente cadastrado com esse nome: ' + erro.error.errors["Nome"],
            { classname: 'bg-danger text-light', delay: 5000 });

        }
      );
  }


  cancelar(): void {
    this.router.navigate(['cliente/listar']);
  }

}
