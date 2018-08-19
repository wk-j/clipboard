module Clipboard

open System
open System.Diagnostics

let run fileName arguments =
    let  info  = ProcessStartInfo()
    info.FileName <- fileName
    info.Arguments <- arguments
    info.RedirectStandardOutput <- true
    info.UseShellExecute <- false
    info.CreateNoWindow <- true

    use ps = new Process()
    ps.StartInfo <- info
    ps.Start() |> ignore
    let result = ps.StandardOutput.ReadToEnd()
    ps.WaitForExit()
    result

let bash (cmd: string) =
    let escapedArgs = cmd.Replace("\"", "\\\"")
    run "/bin/bash"  (sprintf "-c \"%s\"" escapedArgs)

let copy (value: string) =
    (sprintf "echo \"%s\" | tr -d '\n' | pbcopy" value)
    |> bash

[<EntryPoint>]
let main argv =
    if argv.Length <> 0 then
        //copy argv.[0]  |> ignore
        TextCopy.Clipboard.SetText argv.[0]
    else
        let data = Console.ReadLine()
        // copy data |> ignore
        TextCopy.Clipboard.SetText data
    0
