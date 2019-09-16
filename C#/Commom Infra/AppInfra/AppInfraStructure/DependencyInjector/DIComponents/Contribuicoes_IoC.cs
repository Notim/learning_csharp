using SimpleInjector;

using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Core.Converters;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Core.Factories.interfaces;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Core.Factories.services;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Core.Mappers;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Core.Validators;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJMunicipio;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJMunicipio.Converters;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJMunicipio.Mappers;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJMunicipio.Validators;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJProderj;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJProderj.Converters;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJProderj.Mappers;
using WEB.Areas.Contribuicoes.Services.ConversorPadroesContribuicao.Vendors.UFERJProderj.Validators;
using WEB.Areas.Contribuicoes.Services.Interfaces;
using WEB.Areas.Contribuicoes.Services.Services;
using WEB.Areas.Contribuicoes.Services.Tools.ConversorPadroesContribuicao;

namespace WEB.AppInfraStructure.DependencyInjector.DIComponents {

    public static class Contribuicoes_IoC {

        public static void mapear(ref Container container) {

            mapperContribuicoes(ref container);

            mapperConversorContribuicoes(ref container);

        }

        private static void mapperContribuicoes(ref Container container) {
            container.Register<IConsultaContribuicoesApi, ConsultaContribuicoesApi>();
        }

        private static void mapperConversorContribuicoes(ref Container container) {
            container.Register<IConversorPadoresContribuicaoFacade, ConversorPadoresContribuicaoFacade>();

            container.Register<IPatternProviderConversorFactoryRepository, PadraoSistemaConversorFactoryRepository>();

            container.Collection.Register<IPatternProviderConversorFactory>( // quando houver novas Factories de conversão colocar a nova implementação aqui
                 typeof (UFerfProderjFactory),
                 typeof (UferjMunicipioFactory)
            );

            // Padrão UFerf Proderj
            container.RegisterConditional<IPatternProviderValidator, UFerfProderjValidator>(ctx => ctx.Consumer.ImplementationType == typeof (UFerfProderjFactory));
            container.RegisterConditional<IPatternProviderConverter, UFerfProderjConverter>(ctx => ctx.Consumer.ImplementationType == typeof (UFerfProderjFactory));
            container.RegisterConditional<IPatternProviderMapper, UFerfProderjMapper>(ctx => ctx.Consumer.ImplementationType       == typeof (UFerfProderjFactory));

            // Padrão UFerf Municipio
            container.RegisterConditional<IPatternProviderValidator, UferjMunicipioValidator>(ctx => ctx.Consumer.ImplementationType == typeof (UferjMunicipioFactory));
            container.RegisterConditional<IPatternProviderConverter, UferjMunicipioConverter>(ctx => ctx.Consumer.ImplementationType == typeof (UferjMunicipioFactory));
            container.RegisterConditional<IPatternProviderMapper, UferjMunicipioMapper>(ctx => ctx.Consumer.ImplementationType       == typeof (UferjMunicipioFactory));

        }
    }

}