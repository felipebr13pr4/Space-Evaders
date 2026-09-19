# Space Evaders

This repo is for learning. Comments are welcome. I will not be accepting pullings or modifications as that is not the purpose of this repo.

This will probably just be a Space Invaders clone but with some more things.

Develop branch will be where recent changes will be. And most of development.

If you clone this project, be aware that your editor game window resolution must correlate to the available resolutions so fonts won't break.
(1920x1080 (Most Ideal), 1440x810, 960x540, 480x270.)

Project created at "created_at": "2026-08-19T13:39:35Z", according to this repo's data from https://api.github.com/repos/felipebr13pr4/Space-Evaders

Link itch: (I'll add this when i have it which is after the project is entirely done)

# How to play

WASD for movement.
Esc to pause / leave submenus.
R for quick retry.
Kill the squares.
Choose a card each 3 waves.
Survive the longest.

# Development Notes

Fouth time using git and github. Didn't have trouble with git i'd say.
Once again it took longer this time. It was created at "2026-08-19T13:39:35Z", 08/19.
Today's 09/18, so its been a month but of course not all days i worked fully, so i'd say its a little less and of course i didn't work 24 hours a day.
Total project time: 30 days.
Theoretical project time: 26 days.
Actual work time: 10-23 days (only counting when i was actively working on the project).

Re-appearances from last project
- Scope creep.

I learned
- That unity packaging is really good.
- A little more about lists (they're interesting), Coroutines, Inherited static events, using the profiler, optimizing, debugging, dictionaries, events and design philosphy.
- Structs vs classes for small data.
- "is not" and "is" vs "!=" and "==".
- Get Component In Childrens doesn't get unactive children unless you pass true inside it, but it does grab a directly referenced inactive children (Technically i learned this in the last project).
- Coroutines can freeze Unity if caught in a infinite loop inside it without a yield return.
- Using % (remainder operator).
- You can make a "Bridge"/"Wrap" to make a non-IEnumerator event call a IEnumerator method.
- Solutions for something that needs two parents (the multi-aimbot enemy needing aimbot and multi).
- I need to make sure old tacky solutions are still necessary.
- 'Do' before while to use the code inside the while before the while checks.

Minor learnt things
- Getting smallest value from a list.
- How to get the distance from x thing to y thing.
- GameObject can't be caught by get component (it even does a warning) so just use Transform.
- Checking if dict is empty.

Learned but not implemented.
- Continue outer (C# lacks that so here its just a goto statement). I found it interesting.

Next time i should
- Hesitate a bit less to ask for help.
- Think.
- Step up git etiquette.
- Maybe build a method that just delays a frame (I notice alot of times i just need a method to start but be delayed by a single frame).
- Learn how to use debug mode for the red line break things and likely other useful tools.
- Add sounds way way earlier (I realize I keep forgetting and I end up doing it when close to finishing the project). And also other basic things.

Did I actually do it? (Did i do past "Next times i should")
---Broke and Out---
1 Document this section as i go, its easier than having to remember everything i did.
2 Step up git etiquette just like i did in this repo (Maybe trying to do branches for features? And maybe better naming).
3 Search more and not hesitate to ask things to AI.
4 Use AI as a learning tool more (it can be really useful for learning basic things which then you can fuse and build more complex things).
5 Debate and think more about the true weight of an addition, even if it seems small.
---Broke and Out---
---Did i?---
1 Yes.
2 No/Yes. No branches for features. A little bit better naming (debatable).
3 Debatable. I hesitated more but i did ask after all.
4 Debatable.
5 Not as much as I need to do.
---Did i?---

General Thoughts
- Im starting to realize that there must be looots of situations where theres a better solution but i can't know that because i am not aware of that better solution, which also means i likely can't put it here on things i should learn or do next time as i just don't know about said thing.
- Claude's kinda good at catching bugs, I was having a really annoying one with the waves that I basically understood how to make it happen but not why it happened. The solution was kinda simple tbh. Just some troubles with coroutines still going.
- Once again Claude helped me, at optimizing when there are lots of enemies it was a bit laggy and he showed me to cache new waitforseconds, a small thing i forgot and a solution to just not using coroutine in the bullets movement. In general i learned to cache it. Anddd the Resource.Load was being used every time which was bad (that was what i forgot).
- I've created a file called TheGraveyard, i decided to keep old things there for documentations purpose, otherwise i would have just deleted the things as its saved in between these commits anyways. But this file can be used to show differences from developing and the final build.
- Two small bugs i know but won't fix. One is that enemies can go into a same square as the way its set up in a summary is "check, empty? move" but two enemies can look at a empty spot at the same frame and they end up in the same square. I do have a system to make them quickly get out of it though but it happens (and also the average player in a casual playthrough shooouldn't see it i think). I also know the fix is just to make a ghost enemy some frames before it moves so it basically says "im moving there" and others don't move there but thats too much work for something not quite a problem. The other bug is a funny/neat one, when a enemy is completely squared and you pause it's eyes go haywire since the yield return is null. Really easy fix i think, just making it a waitforseconds instead of yield return null in the movement but i liked it so im keeping it.
- One thing i notice is that sometimes im likely overnaming vars. I think i should trust a bit more on context instead. E.g. a CardData inside Card could be just named Data instead of CardData i think. But since i think i've not been doing that i'll not change it to keep the project consistent (alternative is going through the entirety of it searching for those cases).
- I must say, the gameplay isn't the best. Buuut i'd say its because it lacks things to make it more unique. But yeah i could go a "isaac" route if i were to increase the scope and add way more stats, unique unlocks and modifiers, unique enemies, maybe even seeded stuff so its all not the same enemies everytime, that sorta thing.

# Project Plan

Made at the very start of the repo.

Time to make Space Invaders.

Space Invaders core consists of
- A player who can move left and right and shoot lasers up.
- Aliens that slowly come down and shoot down.
- Stationary barriers that block aliens shot that serves as covers.
- Kill all aliens.
- Less aliens = faster aliens.
- You touch aliens/shots = you take damage.

Things for a polished experience
- 16:9 screen.
- All UI essentials, pause button, menu, configs window, main menu.

Ideas for fun?
- Maybe lean into evading? (because of the title name)
- Maybe a upgrade-type game? Where you do a run, eventually die but now you have some sort of money you can buy upgrades and advance more this time?
- Maybe a level game just like the last project?
- Maybe a bullet hell?
- Maybe unique patterns aliens can descend instead of the normal block grid?
- Maybe unique player bullets?
- Maybe other small fun mechanics?

# Credits

Main Menu Music: https://youtu.be/In9xTpfjorU?si=IJYazD4zRjoKC5Yg
8bit Dungeon Boss by Kevin MacLeod

Main Game music: https://youtu.be/OuRvOCf9mJ4?si=evNREZhg_fYb4e6w
MAZE by Density & Time

Font: https://www.dafont.com/pixel-operator.font



# PT BR

# Space Evaders

Este Repo é pra aprendizado. Comentários são bem-vindos. Eu não estarei aceitando pulls ou modificações já que n é o propósito deste repo.

Isto vai provavelmente ser um clone de Space Invaders com umas coisas a mais.

Se você clonar este projeto, saiba que a sua resolução de janela de jogo tem que ser a mesma das disponíveis no jogo para que as fontes não quebrem.
(1920x1080 (Mais Ideal), 1440x810, 960x540, 480x270.)

Projeto criado em "created_at": "2026-08-19T13:39:35Z", de acordo com os dados do repo de https://api.github.com/repos/felipebr13pr4/Space-Evaders

Link do itch: (Vou adicionar aqui quando criar.)

# Como jogar

WASD para mover.
Esc para pausar / sair de submenus.
R para re-tentar rapidamente.
Mate os quadrados.
Escolha uma carta cada 3 ondas.
Sobreviva pelo mais longo.

# Notas de desenvolvimento.

Quarta vez usando git e github. Não tive problemas com git eu diria.
Novamente demorou mais desta vez. Ele foi criado em "2026-08-19T13:39:35Z", 19/08.
Hoje é 18/09, então faz um mes mas claro que não todos dias eu completamente trabalhei, então eu diria q é um pouco menos e claro eu n trabalhei 24 horas por dia.
Tempo de projeto: 30 dias.
Tempo teórico de projeto: 26 dias.
Real tempo de trabalho: 10-23 dias (apenas contando quando eu estava ativamente trabalhando no projeto).

Re-aparências do último projeto
- Problemas de escopo.

Eu aprendi
- Que pacotes do unity é bem bom.
- Um pouco mais sobre listas (são interessantes), Corrotinas, eventos estáticos herançados, usando o profiler, otimização, debugging, dicionarios, eventos e filosofia de design.
- Structs vs classes para dados pequenos.
- "is not" e "is" vs "!=" e "==".
- Get Component In Children não pega crianças inativas a não ser que você passe true dentro dele, mas ele pega uma criança inativa se ela é diretamente referenciada (tecnicamente aprendi isso no ultimo projeto).
- Corrotinas podem congelar o Unity se pego em um loop infinito dentro dele sem um yield return.
- Usar % (operador de sobra).
- Voce pode fazer uma "Ponte/Wrap" para fazer um evento não-IEnumerator chamar um método IEnumerator.
(apartir daqui eu usei o claude pra gerar o resto. Escrever tudo denovo cansa e ficou bom. Beem pequenas modificações "Muitas" -> "Muuuitas" e "Mas" -> "Maaas" pra ser mais proximo do texto original.)
- Soluções para algo que precisa de dois pais (o inimigo multi-aimbot precisando de aimbot e multi).
- Eu preciso ter certeza que soluções temporárias antigas ainda são necessárias.
- 'Do' antes do while pra usar o código dentro do while antes do while checar.

Pequenas coisas aprendidas
- Pegar o menor valor de uma lista.
- Como pegar a distância de uma coisa x pra uma coisa y.
- GameObject não pode ser pego por get component (ele até dá um aviso) então só usar Transform.
- Checar se um dict tá vazio.

Aprendi mas não implementei
- Continue outer (C# não tem isso então aqui é só um goto statement). Achei interessante.

Na próxima vez eu devo
- Hesitar um pouco menos pra pedir ajuda.
- Pensar.
- Melhorar a etiqueta de git.
- Talvez fazer um método que só atrasa um frame (percebo que várias vezes eu só preciso de um método que comece mas atrasado por um frame).
- Aprender a usar o modo debug pras linhas vermelhas de quebra e provavelmente outras ferramentas úteis.
- Adicionar sons bem mais cedo (percebo que sempre esqueço e acabo fazendo isso perto do fim do projeto). E também outras coisas básicas.

Eu de fato fiz? (Eu fiz "Na próxima vez eu devo" passados)
---Broke and Out---
1 Documentar essa seção enquanto eu vou, é mais fácil do que ter que lembrar tudo que eu fiz.
2 Melhorar a etiqueta de git assim como eu fiz nesse repo (Talvez tentando fazer branches pra features? E talvez nomes melhores).
3 Pesquisar mais e não hesitar em perguntar coisas pra IA.
4 Usar IA como ferramenta de aprendizado mais (pode ser bem útil pra aprender coisas básicas que daí você pode fundir e construir coisas mais complexas).
5 Debater e pensar mais sobre o peso real de uma adição, mesmo se parecer pequena.
---Broke and Out---
---Eu fiz?---
1 Sim.
2 Não/Sim. Nenhuma branch pra features. Um pouco melhor nomeação (discutível).
3 Discutível. Hesitei mais mas perguntei no final das contas.
4 Discutível.
5 Não tanto quanto eu precisava.
---Eu fiz?---

Pensamentos Gerais
- Tô começando a perceber que devem ter muuuitas situações onde tem uma solução melhor mas eu não consigo saber disso porque eu não tô ciente dessa solução melhor, o que também significa que eu provavelmente nem consigo colocar isso aqui em coisas que eu devo aprender ou fazer da próxima vez já que eu simplesmente não sei sobre aquilo.
- O Claude é meio bom em pegar bugs, eu tava tendo um bug bem chato com as ondas que eu basicamente entendi como fazer acontecer mas não entendia o porquê. A solução era meio simples na real. Só uns problemas de corrotina ainda rodando.
- De novo o Claude me ajudou, otimizando quando tem muitos inimigos que tava travando um pouco, ele me mostrou pra cachear novos waitforseconds, uma coisa pequena que eu esqueci, e uma solução pra simplesmente não usar corrotina no movimento das balas. Em geral aprendi a cachear isso. E o Resource.Load tava sendo usado toda hora o que era ruim (isso era o que eu tinha esquecido).
- Criei um arquivo chamado TheGraveyard, decidi manter coisas antigas lá, pra fins de documentação, já que senão eu teria só deletado as coisas já que fica salvo entre os commits de qualquer forma. Mas esse arquivo pode ser usado pra mostrar as diferenças do desenvolvimento pro build final.
- Dois bugzinhos que eu sei mas não vou consertar. Um é que inimigos podem ir pro mesmo quadrado já que o jeito que tá montado, resumindo, é "checa, vazio? move" mas dois inimigos podem olhar pra um espaço vazio no mesmo frame e acabam no mesmo quadrado. Eu tenho um sistema pra fazer eles saírem rápido disso mas acontece (e também o jogador casual médio numa jogada normal não deveria nem ver isso eu acho). Eu também sei que o fix é só fazer um inimigo fantasma uns frames antes dele se mover, aí basicamente ele diz "eu vou me mover pra lá" e os outros não vão, mas isso é trabalho demais pra algo que nem chega a ser um problema de verdade. O outro bug é engraçado/maneiro, quando um inimigo tá totalmente encurralado e você pausa, os olhos dele ficam malucos já que o yield return é null. Fix bem fácil eu acho, só fazer um waitforseconds em vez de yield return null no movimento, mas eu gostei e vou deixar assim.
- Uma coisa que eu percebo é que às vezes eu nomeio demais as vars. Acho que eu devia confiar um pouco mais no contexto em vez disso. Tipo, um CardData dentro de Card podia ser só chamado de Data eu acho. Mas já que eu acho que não fiz isso em outros lugares eu não vou mudar, pra manter o projeto consistente (a alternativa é ir procurando por esses casos em tudo).
- Eu tenho que dizer, a jogabilidade não é a melhor. Maaas eu diria que é porque falta coisa pra deixar mais único. Mas é, eu poderia ir na rota "isaac" se eu fosse aumentar o escopo e adicionar bem mais stats, unlocks e modificadores únicos, inimigos únicos, talvez até seed pra não ser sempre os mesmos inimigos toda vez, esse tipo de coisa.

# Planos do Projeto

Feito no início do repo.

Hora de fazer Space Invaders.

O núcleo de Space Invaders consiste em
- Um jogador que pode mover pra esquerda e direita e atirar laser pra cima.
- Alienígenas que descem devagar e atiram pra baixo.
- Barreiras estacionárias que bloqueiam os tiros dos alienígenas que servem de cobertura.
- Matar todos os alienígenas.
- Menos alienígenas = alienígenas mais rápidos.
- Você toca nos alienígenas/tiros = você toma dano.

Coisas pra uma experiência polida
- Tela 16:9.
- Todos os essenciais de UI, botão de pausa, menu, janela de configs, menu principal.

Ideias pra diversão?
- Talvez focar em desviar? (por causa do nome do título)
- Talvez um jogo tipo upgrade? Onde você faz uma run, eventualmente morre mas agora você tem algum tipo de dinheiro que dá pra usar pra comprar upgrades e avançar mais dessa vez?
- Talvez um jogo de níveis igual o último projeto?
- Talvez um bullet hell?
- Talvez padrões únicos que os alienígenas descem em vez da grade normal?
- Talvez balas únicas do jogador?
- Talvez outras mecânicas pequenas divertidas?

# Créditos

Música do Menu Principal: https://youtu.be/In9xTpfjorU?si=IJYazD4zRjoKC5Yg
8bit Dungeon Boss by Kevin MacLeod

Música do Jogo Principal: https://youtu.be/OuRvOCf9mJ4?si=evNREZhg_fYb4e6w
MAZE by Density & Time

Fonte: https://www.dafont.com/pixel-operator.font