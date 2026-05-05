import { ButtonRedirect } from "../../../components/Button/Button"
import { StatCard } from "../../../components/Cards/Cards"
import Statcards from '../../../mocks/statcards.json'
import './Overview.css'

export function OverviewDashboard(){
  const statcards = Statcards
  return(
    <div className="content-section">
      <div className="content-header" style={{flexDirection: 'column',alignItems:'start'}}>
        <h1>Welcome, User</h1>
        <p>Real-time administration panel</p>
      </div>
      <div className="content-main">
        <div className="stat-cards">
          {statcards.map(attribute => (
            <StatCard icon={attribute.icon} title={attribute.title} value={attribute.value} site={attribute.site}/>
          ))}
        </div>
        <div className="quick-access">
          <h2>Quick Access</h2>
          <div className="buttons">
            <ButtonRedirect title={'View Catalog'} site={'/dashboard/my-menu'} classN={'black'}/>
            <ButtonRedirect title={'Manage Locations'} site={'/dashboard/restaurants'} classN={'clear'}/>
          </div>
        </div>
      </div>
    </div>
  )
}