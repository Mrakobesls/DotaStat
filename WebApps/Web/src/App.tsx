import React, {useState} from 'react';
import './App.css';
import AuthProvider from "./Providers/AuthProvider";
import Auth from "./Components/Header/Auth";
import Search from "./Components/Header/Search";
import HeroesDdl from "./Components/Heroes/HeroesDdl";
import Images from "./Images/Images";
import DotaImage from "./Images/DotaImage";

const App: React.FC = () => {
    const [text, setText] = useState("Works");

    // useEffect(() => {
    //     axios.get(`/TextOk`)
    //         .then((response) => {
    //             setText(response.data);
    //         });
    // }, []);

    return <AuthProvider>
        <div className="App">
            <header className="App-header">
                <Search/>
                <Auth/>
            </header>
            <div className="App-body">
                <div className="introduction-information">
                    <div className="introduction-information__patch">
                        7.35b meta sheets
                    </div>
                    <div className="introduction-information__list">
                        INFO Data: 8000+ MMR, 8 days, minimum 15 minutes duration.
                    </div>
                </div>
                <HeroesDdl/>
                <DotaImage path={Images.heroes("Arc_Warden").icon}/>
            </div>
        </div>
    </AuthProvider>
}

export default App;
