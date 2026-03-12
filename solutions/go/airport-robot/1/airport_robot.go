package airportrobot

import "fmt"

type Greeter interface {
    Language() string
    Greet(name string) string
}

func SayHello(name string, greeter Greeter) string {
    return fmt.Sprintf("I can speak %s: %s", greeter.Language(), greeter.Greet(name))
}

type Italian struct {}

func (g Italian) Language() string {
    return "Italian"
}

func (g Italian) Greet(name string) string {
    return fmt.Sprintf("Ciao %s!", name)
}

type Portuguese struct {}

func (g Portuguese) Language() string {
    return "Portuguese"
}

func (g Portuguese) Greet(name string) string {
    return fmt.Sprintf("Olá %s!", name)
}
