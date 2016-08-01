module ImagePickerScene

open System
open Fable.Core
open Fable.Import
open Fable.Import.ReactNative
open Fable.Import.ReactNativeImagePicker
open Fable.Helpers.ReactNative
open Fable.Helpers.ReactNative.Props
open Fable.Helpers.ReactNativeImagePicker
open Fable.Helpers.ReactNativeImagePicker.Props

type IP = ReactNativeImagePicker.Globals

type ImagePickerSceneProperties = {
    title: string
    onDone: unit -> unit
}

type ImagePickerState = {
    uri: string
}

let baseUrl = "http://facebook.github.io/react/img/logo_og.png"

type ImagePickerScene (props) as this =
    inherit React.Component<ImagePickerSceneProperties,ImagePickerState>(props)

    do this.state <- { uri = baseUrl }

    member x.render () =
        let selectImageButton =
            text [] "Click me to select image!"
            |> touchableHighlight [
                TouchableHighlightProperties.Style [
                    ViewStyle.BackgroundColor "#AA00AA"
                    ViewStyle.Flex 1
                 ]
                OnPress 
                    (fun () ->
                        (showImagePicker
                            [Title "Image picker"; AllowsEditing true] 
                            (fun result -> 
                                x.setState { 
                                    uri = 
                                        if not result.didCancel then
                                            if String.IsNullOrEmpty result.error then
                                                result.uri
                                            else
                                                result.error
                                        else
                                            baseUrl })))]

        let doneButton =
            text [] "Tap me to go back"
            |> touchableHighlight [
                TouchableHighlightProperties.Style [
                    ViewStyle.BackgroundColor "#AA00AA"
                    ViewStyle.Flex 2
                 ]
                OnPress x.props.onDone]

        let image =
            image 
                [ Source [ Uri x.state.uri; IsStatic true]
                  ImageProperties.Style [
                    ImageStyle.BorderColor "#000000"
                    ImageStyle.Flex 3
                  ]
                ]

        view [ ViewProperties.Style [ViewStyle.Flex 1]] 
            [ image
              selectImageButton
              doneButton ]    
