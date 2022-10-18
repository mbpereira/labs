## Objetivo
O objetivo desse teste é entender como o uso de .Wait() ou .Result bloqueia a thread e acaba com a ideia de async/await.

## Descrição do teste

A ideia desse teste é basicamente iniciar 'Task' e um processamento qualquer 
e só então aguardar a finalização da Task iniciada anteriormente.

```cs
// LabDotNet.AsyncAwait.Program.MakeAnything
async static Task Work(Func<Task> startTask)
{
	var task = startTask();
	var processTask = StartProcessAsync();

	var tasks = new[] { task, processTask };

	await Task.WhenAll(tasks);
}
```

Com essa abordagem, é esperado que a Task e o processamento sejam executados praticamente juntos. 
Entretanto, se a função startTask utilizar na sua implementação .Wait() ou .Result, isso não acontecerá,
uma vez que essas instruções são 'bloqueantes'. Sua utilização acaba com objetivo do async/await, pois transforma um
código que deveria ser assincrono em sincrono.


## Resultados
### Utilizando métodos bloqueantes

```cs
Console.WriteLine($"{nameof(BlockingProcessAsync)} Execution:");
await Work(BlockingProcessAsync);
```

Através da saída abaixo podemos verificar que mesmo utilizando async/await e Task.WhenAll,
o trabalho foi executado de forma *Sincrona*:
```bash
BlockingProcessAsync Execution:
delaying 0 * 500 = 0
delaying 1 * 500 = 500
delaying 2 * 500 = 1000
delaying 3 * 500 = 1500
delaying 4 * 500 = 2000
delaying 5 * 500 = 2500
delaying 6 * 500 = 3000
delaying 7 * 500 = 3500
delaying 8 * 500 = 4000
delaying 9 * 500 = 4500
Working... 0
Working... 500
Working... 1000
Working... 1500
Working... 2000
Working... 2500
Working... 3000
Working... 3500
Working... 4000
Working... 4500
```

### Utilizando métodos não bloqueantes
```cs
Console.WriteLine($"{nameof(NonBlockingProcessAsync)} Execution: ");
await Work(NonBlockingProcessAsync);
```

Utilizando métodos não bloqueantes, podemos verificar que o trabalho foi executado de forma 
assincrona, como esperado:
```bash
NonBlockingProcessAsync Execution:
delaying 0 * 500 = 0
delaying 1 * 500 = 500
Working... 0
Working... 500
Working... 1000
delaying 2 * 500 = 1000
Working... 1500
delaying 3 * 500 = 1500
Working... 2000
delaying 4 * 500 = 2000
Working... 2500
delaying 5 * 500 = 2500
Working... 3000
delaying 6 * 500 = 3000
Working... 3500
delaying 7 * 500 = 3500
Working... 4000
delaying 8 * 500 = 4000
delaying 9 * 500 = 4500
Working... 4500
```
