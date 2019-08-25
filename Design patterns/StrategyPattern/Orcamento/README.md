## Padrao strategy

Quando usar:
quando se faz necessario usar muitas condiçoes especificas para uma determinada tarefa ou regra de negocio.

porque usar:
eh uma convenção, ce usa se quiser bro, so nao reclama dos ifs encadeados e um monte de codigo confuso que ce nao 
vai entender depois taok!

Como usar:
Criar uma interface que implemente metodos que sera padrao para todos essas condicoes especificas

Problema exemplo:
Realizador de orcamento com Calculador de impostos.
imagina onde exista uma caralhada de tipos de impostos, e que basicamente ele faz a mesma coisa no final que é 
ser somado no valor total do orcamento. Foi criado uma interface chamada ITax que possui um metodo chamado Calcular.
Pra cada tipo de imposto diferente vc implementa a interface e usa o metodo pra realizar as regras inclusive usar 
recursos externos 
