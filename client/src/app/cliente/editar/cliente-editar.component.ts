import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IHttpClienteService } from 'src/app/shared/interfaces/IHttpClienteService';
import { ClienteType } from 'src/app/shared/models/ClienteEnum';
import { ToastService } from 'src/app/shared/services/toast.service';
import { ClienteDetailsViewModel } from 'src/app/shared/viewModels/cliente/ClienteDetailsViewModel';
import { ClienteEditViewModel } from 'src/app/shared/viewModels/cliente/ClienteEditViewModel';

@Component({
  selector: 'app-cliente-editar',
  templateUrl: './cliente-editar.component.html',
})
export class ClienteEditarComponent implements OnInit {

  cadastroForm: FormGroup;
  id: any;
  cliente: ClienteEditViewModel;

  tipos = ClienteType;
  chaves: any[];


  constructor(private _Activatedroute: ActivatedRoute, @Inject('IHttpClienteServiceToken') private servicoCliente: IHttpClienteService,
    private router: Router,
    private toastService: ToastService) { }

  ngOnInit(): void {

    this.id = this._Activatedroute.snapshot.paramMap.get("id");

    this.chaves = Object.keys(this.tipos).filter(t => !isNaN(Number(t)));

    this.cadastroForm = new FormGroup({
      nome: new FormControl(''),
      endereco: new FormControl(''),
      telefone: new FormControl(''),
      rg: new FormControl(''),
      cpf: new FormControl(''),
      cnpj: new FormControl(''),
      email: new FormControl(''),
      tipo: new FormControl('')
    });

    this.carregarClientes();
  }

  carregarClientes(): void {
    this.servicoCliente.obterClientePorId(this.id)
      .subscribe((cliente: ClienteDetailsViewModel) => {
        this.carregarFormulario(cliente);
      });

  }

  carregarFormulario(cliente: ClienteDetailsViewModel) {

    this.cadastroForm = new FormGroup({
      nome: new FormControl(cliente.nome, Validators.required),
      endereco: new FormControl(cliente.endereco, Validators.required),
      telefone: new FormControl(cliente.telefone, Validators.required),
      rg: new FormControl(cliente.rg, Validators.required),
      cpf: new FormControl(cliente.cpf, Validators.required),
      cnpj: new FormControl(cliente.cnpj),
      email: new FormControl(cliente.email, Validators.required),
      tipo: new FormControl(cliente.tipoPessoa)
    });
  }
  atualizarCliente() {
    this.cadastroForm.valid
    this.cliente = Object.assign({}, this.cliente, this.cadastroForm.value);
    this.cliente.id = this.id;

    this.servicoCliente.editarCliente(this.cliente)
      .subscribe(
        cliente => {
          this.toastService.show('Cliente ' + cliente.nome + ' editado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['cliente/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Erro ao editar cliente: ' + erro.error.errors["Nome"].toString(),
            { classname: 'bg-danger text-light', delay: 5000 });
        });
  }

  cancelar(): void {
    this.router.navigate(['cliente/listar']);
  }



}
