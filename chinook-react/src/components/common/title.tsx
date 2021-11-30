type Props = {
    text: string
}

export default function Title(props: Props): JSX.Element {
    return <h1>{props.text}</h1>
}