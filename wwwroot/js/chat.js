console.log("CHAT_JS_VERSION_GROQ_FIXED_001");

document.addEventListener('DOMContentLoaded', () => {
    const input = document.getElementById('userInput');

    input.addEventListener('keydown', e => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            sendMessage();
        }
    });
});

async function sendMessage() {
    const inp = document.getElementById('userInput');
    const msg = inp.value.trim();

    if (!msg) return;

    setEnabled(false);
    addBubble(msg, 'user');
    inp.value = '';

    const typing = addTyping();

    try {
        const res = await fetch('/api/chat/send', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ message: msg })
        });

        const text = await res.text();
        typing.remove();

        if (!text || text.trim() === '') {
            addBubble("Empty response from server. Status Code: " + res.status, 'bot', true);
            return;
        }

        let data;
        try {
            data = JSON.parse(text);
        } catch {
            addBubble("Server returned non-JSON response: " + text, 'bot', true);
            return;
        }

        addBubble(data.reply || "No reply received.", 'bot');

    } catch (err) {
        typing.remove();
        addBubble("Frontend Error: " + err.message, 'bot', true);
    } finally {
        setEnabled(true);
        inp.focus();
    }
}

function addBubble(text, sender, isErr = false) {
    const wrapper = document.createElement('div');
    wrapper.className = 'message ' + (sender === 'user' ? 'user-message' : 'bot-message');

    const bubble = document.createElement('div');
    bubble.className = 'bubble' + (isErr ? ' err-bubble' : '');
    bubble.textContent = text;

    wrapper.appendChild(bubble);
    document.getElementById('chatMessages').appendChild(wrapper);
    scroll();
}

function addTyping() {
    const wrapper = document.createElement('div');
    wrapper.className = 'message bot-message';

    const bubble = document.createElement('div');
    bubble.className = 'bubble typing-bubble';
    bubble.textContent = 'Ojaly is typing...';

    wrapper.appendChild(bubble);
    document.getElementById('chatMessages').appendChild(wrapper);
    scroll();

    return wrapper;
}

function scroll() {
    const el = document.getElementById('chatMessages');
    el.scrollTop = el.scrollHeight;
}

function setEnabled(on) {
    document.getElementById('userInput').disabled = !on;
    document.getElementById('sendBtn').disabled = !on;
}
