import axios from "axios"

export type Track = {
    "trackId": number,
    "name": string,
    "albumId": number,
    "mediaTypeId": number,
    "genreId": number,
    "composer": string,
    "milliseconds": number,
    "bytes": number,
    "unitPrice": number,
}

export async function getTrackById(id: number): Promise<Track> {
    // Obtener la data con axios
    const result = await axios.get<Track>(`https://localhost:5001/tracks/${id}`)
    const body = result.data

    return body;
}