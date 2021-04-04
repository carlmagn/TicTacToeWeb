const howToPlayMessages = [
    { message: "Really?... You don't know how to play tic-tac-toe? My oh my..." },
    { message: "I'm not sure you can be helped. Good luck with life tho, you'll need it." },
    { message: "C'mon man stop bothering me. You seem like a smart kid, go figure it out..." },
    { message: "Nope" },
    { message: "You're a persistent son of a bitch huh? Okay let me see what i can find... Oh, here try this: ", link: "https://www.youtube.com/watch?v=oHg5SJYRHA0&ab_channel=cotter548" },
    { message: "Hehe got you." },
    { message: "Fine, here is the real link: ", link: "https://www.exploratorium.edu/brain_explorer/tictactoe.html" }
];
let messageIndex = 0;

$(() => {
    $('body').click(() => {
        var popupIsVisible = isHidden(document.getElementById('popup-container'))
        if (popupIsVisible) {
            hideHowToPlay();
        }
    });
    $('.popup-container').click((e) => e.stopPropagation());
    $('#how-to-play-button').click((e) => e.stopPropagation())
})

function setHowToPlayMessage() {
    let { message, link } = howToPlayMessages[messageIndex];

    messageIndex += messageIndex === howToPlayMessages.length - 1 ? 0 : 1;

    $('#how-to-play-text').text(message);

    $('#how-to-play-link').text(link ? 'How to play tic-tac-toe' : '');
    $('#how-to-play-link').attr('href', link);

    showHowToPlay();
}

function isHidden(element) {
    return !(element.offsetParent === null)
}

const showHowToPlay = () => $('.popup-container').show();
const hideHowToPlay = () => $('.popup-container').hide();