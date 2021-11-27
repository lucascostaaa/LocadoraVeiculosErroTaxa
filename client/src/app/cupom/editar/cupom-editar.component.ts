import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { IHttpCupomService } from 'src/app/shared/interfaces/IHttpCupomService';
import { IHttpParceiroService } from 'src/app/shared/interfaces/IHttpParceiroService';
import { Cupom } from 'src/app/shared/models/Cupom';
import { CupomType } from 'src/app/shared/models/CupomEnum';
import { Parceiro } from 'src/app/shared/models/Parceiro';
import { ToastService } from 'src/app/shared/services/toast.service';
import { valorMinimo } from 'src/app/shared/validators/valor-cupom.directive';
import { CupomDetailsViewModel } from 'src/app/shared/viewModels/cupom/CupomDetailsViewModel';
import { CupomEditViewModel } from 'src/app/shared/viewModels/cupom/CupomEditViewModel';
import { ParceiroListViewModel } from 'src/app/shared/viewModels/parceiro/ParceiroListViewModel';

@Component({
  selector: 'app-cupom-editar',
  templateUrl: './cupom-editar.component.html'
})
export class CupomEditarComponent implements OnInit {

  cadastroForm: FormGroup;
  id: any;
  cupom: CupomEditViewModel;
  listaParceiros: ParceiroListViewModel[];

  tipos = CupomType;
  chaves: any[];

  constructor(private _Activatedroute: ActivatedRoute,
    @Inject('IHttpCupomServiceToken') private servicoCupom: IHttpCupomService,
    @Inject('IHttpParceiroServiceToken') private servicoParceiro: IHttpParceiroService,
    private router: Router,
    private toastService: ToastService) { }

  ngOnInit(): void {
    this.id = this._Activatedroute.snapshot.paramMap.get("id");

    this.chaves = Object.keys(this.tipos).filter(t => !isNaN(Number(t)));

    this.cadastroForm = new FormGroup({
      nome: new FormControl(''),
      valor: new FormControl(''),
      valorMinimo: new FormControl(''),
      dataValidade: new FormControl(''),
      parceiroId: new FormControl(''),
      tipo: new FormControl('')
    });

    this.carregarParceiros();
    this.carregarCupom();
  }

  carregarParceiros(): void {
    this.servicoParceiro.obterParceiros()
      .subscribe(parceiros => {
        this.listaParceiros = parceiros;
      });
  }

  carregarCupom(): void {
    this.servicoCupom.obterCupomPorId(this.id)
      .subscribe((cupom: CupomDetailsViewModel) => {
        this.carregarFormulario(cupom);
      });
  }

  carregarFormulario(cupom: CupomDetailsViewModel) {

    this.cadastroForm = new FormGroup({
      nome: new FormControl(cupom.nome, Validators.required),
      valor: new FormControl(cupom.valor,  Validators.compose([Validators.required, Validators.min(1), valorMinimo])),
      valorMinimo: new FormControl(cupom.valorMinimo, Validators.compose([Validators.required, valorMinimo])),
      dataValidade: new FormControl(cupom.dataValidade.toLocaleString().substring(0, 10), Validators.required),
      parceiroId: new FormControl(cupom.parceiroId, Validators.required),
      tipo: new FormControl(cupom.tipo)
    });
  }

  atualizarCupom() {
    this.cadastroForm.valid
    this.cupom = Object.assign({}, this.cupom, this.cadastroForm.value);
    this.cupom.id = this.id;

    this.servicoCupom.editarCupom(this.cupom)
      .subscribe(
        cupom => {
          this.toastService.show('Cupom ' + cupom.nome + ' editado com sucesso!',
            { classname: 'bg-success text-light', delay: 5000 });
          setTimeout(() => {
            this.router.navigate(['cupom/listar']);
          }, 5000);
        },
        erro => {
          this.toastService.show('Erro ao editar cupom: ' + erro.error.errors["Nome"].toString(),
            { classname: 'bg-danger text-light', delay: 5000 });
        });
  }

  cancelar(): void {
    this.router.navigate(['cupom/listar']);
  }
}
