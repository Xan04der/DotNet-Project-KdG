const artworkIdInput = document.getElementById("artworkId")
const artistTableBody = document.getElementById("artistsTableBody")
const artistSelect = document.getElementById("artistSelect")
const timeFrameInput = document.getElementById("timeFrameInput")
const tutorCheckBox = document.getElementById("tutorCheckBox")
const applyBtn = document.getElementById("applyButton")

async function retrieveConnectedArtists() {
    const response = await fetch(
        `/api/Artworks/${artworkIdInput.value}/artists`,
        {
            headers: {
                "Accept" : "application/json"
            }
        }
    )
    if (response.status === 200) {
        artistTableBody.innerHTML = ' '
        
        const artists = await response.json()
        for (const artist of artists) {
            artistTableBody.innerHTML +=`
            <tr>
                <td>${artist.name}</td>
                <td>${artist.timeFrame}</td>
                <td>${artist.tutor}</td>
            </tr>
            `
        }
    }
}

async function retrieveAllArtists() {
    const response = await fetch(
        '/api/Artists',
        {
            headers: {
                "Accept" : "application/json"
            }
        }
    )
    if (response.status === 200) {
        artistSelect.innerHTML = ' '
        
        const artists = await response.json()
        for (const artist of artists) {
            artistSelect.innerHTML +=`
            <option value="${artist.artistId}">${artist.firstName} ${artist.lastName}</option>`
        }
    }
}

async function addArtistToArtwork(){
    const response = await fetch(
        `/api/Artworks/${artworkIdInput.value}/artists`,{
            method: "POST",
            headers: {
                "Accept" : "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                artistId: artistSelect.value,
                timeFrame: timeFrameInput.value,
                tutor: tutorCheckBox.checked,
            })
        }
    )
    if (response.status === 201) {
        retrieveConnectedArtists()
    }
}

retrieveConnectedArtists()
retrieveAllArtists()

applyBtn.addEventListener("click", addArtistToArtwork)