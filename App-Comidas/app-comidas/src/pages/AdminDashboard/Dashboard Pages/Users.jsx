import { ButtonAction } from '../../../components/Button/Button'
import { CrudCard } from '../../../components/Cards/Cards'
import Users from '../../../mocks/users.json'

export function UsersDashboard(){
  return(
    <div className="content-section">
      <div className="content-header">
        <h1>Users</h1>
        <ButtonAction icon={'plus'} classN={'add'} />
      </div>
      <div className="content-main">
        {Users.map((user) => (
          <CrudCard
            img={user.img}
            attribute={user.value}
            icon={'briefcase'}
            name={user.title}
          />
        ))}
      </div>
    </div>
  )
}